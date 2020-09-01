using DataAccess.Model;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Required(ErrorMessage = "Location Required")]
        public string Location { get; set; }

        public ModalMessage Message { get; set; }
    }

    public class TeamMember
    {
        [Required(ErrorMessage = "Field Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }
    }

    public class ModalMessage
    {
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
