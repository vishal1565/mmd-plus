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

        public GameApiService(DataContext context, ILogger<GameApiService> logger)
        {
            _context = context ?? throw new ArgumentNullException("DataContext");
            _logger = logger ?? throw new ArgumentNullException("DataContext");
        }

        public async Task<GameStatusResponse> GetCurrentStatus()
        {
            GameStatusResponse response = new GameStatusResponse();
            try
            {
                var currentPhase = await _context.Phases.OrderByDescending(p => p.TimeStamp).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Fetching current gamestatus failed, {ex}");
                throw new ServerSideException("Failed in GetCurrentStatus()", ex);
            }
            return response;
        }
    }
}