using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Abstract
{
    public interface IUserRepository
    {
        bool IsEmailUniq(string email);

        bool IsEmailUniq(string email, string teamId);
    }
}
