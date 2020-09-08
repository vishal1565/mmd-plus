using DataAccess.Data.Abstract;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public bool IsEmailUniq(string email)
        {
            var user = this.GetSingle(u => u.UserId == email);
            return user == null;
        }

        public bool IsEmailUniq(string email, string teamId)
        {
            var user = this.GetSingle(u => u.UserId == email);
            return user == null || user.TeamId == teamId;
        }
    }
}
