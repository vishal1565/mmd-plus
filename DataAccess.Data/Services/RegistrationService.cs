using DataAccess.Data.Abstract;
using DataAccess.Data.Repositories;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    }
}
