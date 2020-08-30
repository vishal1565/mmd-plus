using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mmd_plus.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "TeamId Required")]
        public string TeamId { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public string Location { get; set; }
    }

    public class TeamMember
    {
        [Required(ErrorMessage = "Email Id Required")]
        [EmailAddress]
        public string EmailId { get; set; }
    }
}
