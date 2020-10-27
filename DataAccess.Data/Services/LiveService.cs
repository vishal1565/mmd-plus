using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataAccess.Data.Abstract;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using Fare;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SendGrid;

namespace DataAccess.Data.Services
{
    public class LiveService : ILiveService
    {

        private readonly DataContext _context;
        private readonly ILogger<LiveService> _logger;

        public LiveService(DataContext context, ILogger<LiveService> logger)
        {
            _context = context ?? throw new ArgumentNullException("DataContext");
            _logger = logger ?? throw new ArgumentNullException("DataContext");
        }

        public virtual async Task<LiveResponse> GetLiveStatus() {

            try
            {
                LiveResponse response = new LiveResponse();

                var currentPhase = await _context.Phases.Include(p => p.Round).OrderByDescending(p => p.TimeStamp).FirstOrDefaultAsync();
                if (currentPhase == null)
                {
                    _logger.LogError("Error : Game not running");
                }
                else
                {
                    var roundConfig = await _context.RoundConfigs.Where(rc => rc.Id == currentPhase.Round.RoundNumber).SingleOrDefaultAsync();
                    var participants = await _context.Participants.Include(p => p.Team).Where(p => p.RoundId.CompareTo(currentPhase.RoundId) == 0).ToDictionaryAsync(p => p.TeamId);

                    var killNumber = await (from k in _context.Kills.Where(k => k.RoundId.CompareTo(currentPhase.RoundId) == 0)
                                            group k by k.VictimId into killGroup
                                            select new
                                            {
                                                TeamId = killGroup.Key,
                                                TotalKills = killGroup.Count()
                                            }).ToDictionaryAsync(kg => kg.TeamId);


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

                    var participantsList = new List<LiveParticipantDetails>();
                    foreach (var key in participants.Keys)
                    {

                        bool inScore = scores.ContainsKey(key);
                        int LifelinesRemaining = roundConfig.LifeLines - (killNumber.ContainsKey(key) ? killNumber[key].TotalKills : 0);
                        bool zeroLifelines = LifelinesRemaining <= 0 ? true : false;

                        participantsList.Add(new LiveParticipantDetails
                        {
                            TeamId = key,
                            CurrentScore = inScore ? scores[key].CurrentScore : 0,
                            TotalScore = inScore ? scores[key].TotalScore : 0,
                            IsAlive = participants[key].IsAlive,
                            IsRobot = participants[key].Team.IsRobot,
                            Location = participants[key].Team.Location,
                            Lifelines = zeroLifelines ? 0 : LifelinesRemaining,
                            RoundId = currentPhase.Round.RoundId,
                            GameId = currentPhase.Round.GameId,
                            RoundNumber = currentPhase.Round.RoundNumber,
                            Phase = currentPhase.PhaseType.ToString()
                        }) ;
                    }


                    response.RoundId = currentPhase.Round.RoundId;
                    response.GameId = currentPhase.Round.GameId;
                    response.RoundNumber = currentPhase.Round.RoundNumber;
                    response.SecretLength = roundConfig?.SecretLength;
                    response.Participants = participantsList;
                    response.Phase = currentPhase.PhaseType.ToString();
                    response.JoiningDuration = roundConfig.JoiningDuration;
                    response.RunningDuration = roundConfig.RunningDuration;
                    response.FinishedDuration = roundConfig.FinishedDuration;
                    response.PhaseStartTime = currentPhase.TimeStamp;

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
