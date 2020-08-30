using DataAccess.Data.Abstract;
using DataAccess.Data.Repositories;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Data.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly DataContext _context;
        private readonly IEntityBaseRepository<Team> _teamBaseRepo;
        private readonly IEntityBaseRepository<User> _userBaseRepo;
        private readonly ILogger<RegistrationService> _logger;

        public RegistrationService(DataContext context, IEntityBaseRepository<Team> teamBaseRepo, IEntityBaseRepository<User> userBaseRepo, ILogger<RegistrationService> logger)
        {
            _context = context;
            _teamBaseRepo = teamBaseRepo;
            _userBaseRepo = userBaseRepo;
            _logger = logger;
        }

        public List<RegisteredTeam> GetRegisteredTeams(Dictionary<string, string> searchValues, string sortColumn, string sortDir, int start, int length, out int filteredCount, out int totalCount)
        {
            IQueryable<Team> query = null;

            totalCount = _teamBaseRepo.Count();

            query = _context.Teams.Include(team => team.LocationNav);

            query = GetQuery(query, sortColumn, sortDir);

            foreach(var x in searchValues)
            {
                query = AddSearchParameter(query, x.Key, x.Value.ToLowerInvariant());
            }

            filteredCount = query.Count();

            query = query.Skip(start).Take(length);

            var result = query.ToList();

            var listOfRegisteredTeams = new List<RegisteredTeam>();

            foreach (var team in result)
                listOfRegisteredTeams.Add(GetTeamDisplayModel(team));

            return listOfRegisteredTeams;
        }

        private RegisteredTeam GetTeamDisplayModel(Team item)
        {
            return new RegisteredTeam
            {
                TeamId = item.TeamId,
                Location = item.LocationNav.DisplayName,
                RegistrationTime = item.RegisteredAt
            };
        }

        private IQueryable<Team> AddSearchParameter(IQueryable<Team> query, string key, string value)
        {
            IQueryable<Team> _query = query;
            switch(key)
            {
                case "id": return query.Where(t => t.Id.ToString().Contains(value));
                case "teamId": return query.Where(t => t.TeamId.ToLowerInvariant().Contains(value));
                case "location": return query.Where(t => t.Location.ToLowerInvariant().Contains(value));
                case "registrationTime": return query.Where(t => t.RegisteredAt.ToString().Contains(value));
            }
            return _query;
        }

        private IQueryable<Team> GetQuery(IQueryable<Team> query, string sortColumn, string sortDir)
        {
            if(sortDir == "asc")
            {
                switch (sortColumn)
                {
                    case "id": return query.OrderBy(t => t.Id);
                    case "teamId": return query.OrderBy(t => t.TeamId);
                    case "location": return query.OrderBy(t => t.Location);
                    case "registrationTime": return query.OrderBy(t => t.RegisteredAt);
                }
            }
            else
            {
                switch (sortColumn)
                {
                    case "id": return query.OrderByDescending(t => t.Id);
                    case "teamId": return query.OrderByDescending(t => t.TeamId);
                    case "location": return query.OrderByDescending(t => t.Location);
                    case "registrationTime": return query.OrderByDescending(t => t.RegisteredAt);
                }
            }
            return query;
        }

        public string RegisterOrUpdate(RegisterTeam validRegisterTeam)
        {
            try
            {
                string msg = null;

                var alreadyRegisteredTeam = _teamBaseRepo.GetSingle(team => team.TeamId == validRegisterTeam.TeamId, team => team.Users);

                if (alreadyRegisteredTeam == null)
                {
                    // Register New Team
                    var newTeam = new Team
                    {
                        TeamId = validRegisterTeam.TeamId,
                        Location = validRegisterTeam.Location,
                        RegisteredAt = validRegisterTeam.RequestTime,
                        LastUpdatedAt = validRegisterTeam.RequestTime,
                        SecretToken = Guid.NewGuid().ToString("N"),
                    };

                    var Users = new List<User>();

                    foreach (var user in validRegisterTeam.TeamMembers)
                        Users.Add(new User { UserId = user, AddedAt = validRegisterTeam.RequestTime });

                    newTeam.Users = Users;

                    _teamBaseRepo.Add(newTeam);

                    _teamBaseRepo.Commit();

                    msg = "Registration Success";
                }
                else
                {
                    // Update Already Registered Team

                    // Delete all current users
                    foreach (var user in alreadyRegisteredTeam.Users)
                        _userBaseRepo.Delete(user);

                    // Add all new users
                    foreach (var user in validRegisterTeam.TeamMembers)
                        _userBaseRepo.Add(new User { UserId = user, AddedAt = validRegisterTeam.RequestTime, TeamId = alreadyRegisteredTeam.TeamId });

                    alreadyRegisteredTeam.LastUpdatedAt = validRegisterTeam.RequestTime;

                    _teamBaseRepo.Commit();

                    msg = "Team Updated";
                }

                return msg;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Registration Failed | {validRegisterTeam.TeamId} | {validRegisterTeam.Location}");
                _logger.LogError($"{ex}");
                return null;
            }
        }

        public List<string> GetLocations()
        {
            return _context.Locations.Select(loc => loc.DisplayName).ToList();
        }
    }
}
