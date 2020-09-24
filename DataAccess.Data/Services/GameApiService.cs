using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Data.Services
{
    public class GameApiService : IGameApiService
    {
        private readonly DataContext _context;
        private readonly ILogger<GameApiService> _logger;
        private readonly RequestContext _requestContext;

        public GameApiService(DataContext context, ILogger<GameApiService> logger, RequestContext requestContext)
        {
            _context = context ?? throw new ArgumentNullException("DataContext");
            _logger = logger ?? throw new ArgumentNullException("DataContext");
            _requestContext = requestContext ?? throw new ArgumentNullException("RequestContext");
        }

        public virtual async Task<GameStatusResponse> GetCurrentStatus()
        {
            try
            {
                GameStatusResponse response = new GameStatusResponse()
                {
                    RequestId = _requestContext.RequestId
                };
                var currentPhase = await _context.Phases.Include(p => p.Round).OrderByDescending(p => p.TimeStamp).FirstOrDefaultAsync();
                if(currentPhase == null)
                {
                    response.Err = new Error
                    {
                        Message = "Try after some time",
                        Description = "Game is not running"
                    };
                }
                else
                {
                    var roundConfig = await _context.RoundConfigs.Where(rc => rc.Id == currentPhase.Round.RoundNumber).SingleOrDefaultAsync();
                    var participants = await _context.Participants.Where(p => p.RoundId.CompareTo(currentPhase.RoundId) == 0).ToDictionaryAsync(p => p.TeamId);
                    
                    var scores = await (from t in (from s in _context.Scores.Where(s => s.GameId.CompareTo(currentPhase.GameId) == 0)
                                                   group s by s.TeamId into scoreGroup
                                                   select new
                                                   {
                                                       TeamId = scoreGroup.Key,
                                                       TotalScore = scoreGroup.Sum(sg => sg.PointsScored)
                                                   })
                                        join c in (from k in _context.Scores.Where(s => s.GameId.CompareTo(currentPhase.GameId) == 0)
                                                   group k by new { k.TeamId, k.RoundId } into scoreGroup
                                                   select new
                                                   {
                                                       TeamId = scoreGroup.Key.TeamId,
                                                       scoreGroup.Key.RoundId,
                                                       CurrentScore = scoreGroup.Sum(sg => sg.PointsScored)
                                                   })
                                        on t.TeamId equals c.TeamId
                                        select new
                                        {
                                            TeamId = t.TeamId,
                                            RoundId = c.RoundId,
                                            CurrentScore = c.CurrentScore,
                                            TotalScore = t.TotalScore
                                        }).Where(q => q.RoundId.CompareTo(currentPhase.RoundId) == 0).ToDictionaryAsync(sg => sg.TeamId);

                    var participantsList = new List<TeamData>();
                    foreach(var key in participants.Keys)
                    {
                        participantsList.Add(new TeamData
                        {
                            TeamId = key,
                            CurrentScore = scores[key].CurrentScore,
                            TotalScore = scores[key].TotalScore,
                            IsAlive = participants[key].IsAlive
                        });
                    }
                    response.Data = new GameStatusResponseData
                    {
                        RoundId = currentPhase.Round.RoundId,
                        GameId = currentPhase.Round.GameId,
                        RoundNumber = currentPhase.Round.RoundNumber,
                        SecretLength = roundConfig?.SecretLength,
                        Participants = participantsList,
                        Status = currentPhase.PhaseType.ToString()
                    };
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fetching current gamestatus failed, {ex}");
                throw new ServerSideException("Failed in GetCurrentStatus()", ex);
            }
        }
    }
}