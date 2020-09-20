using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data.Services
{
    public class GameApiService
    {
        private readonly DataContext _context;

        public GameApiService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetAllTeamAsync()
        {
            return await _context.Teams.ToListAsync();
        }
    }
}