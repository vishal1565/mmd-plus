using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data.Services
{
    public class GameApiService : IGameApiService
    {
        private readonly DataContext _context;

        public GameApiService(DataContext context)
        {
            _context = context;
        }

        public object GetCurrentStatus()
        {
            throw new NotImplementedException();
        }
    }
}