using DataAccess.Data.Abstract;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Repositories
{
    public class TeamRepository : EntityBaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(DataContext context) : base(context) { }

        public bool isTeamIdUnique(string TeamId)
        {
            var team = GetSingle(team => team.TeamId == TeamId);
            return team == null;
        }
    }
}
