using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model.SharedModels
{
    public class RegisterTeam
    {
        public string TeamId { get; set; }
        public List<string> TeamMembers { get; set; }
        public string Location { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
