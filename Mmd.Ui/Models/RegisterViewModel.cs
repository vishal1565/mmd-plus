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
        [Required(ErrorMessage = "TeamId Required"), StringLength(20, ErrorMessage = "Atleast 5 and max 20 characters allowed", MinimumLength = 5)]
        [RegularExpression("^(?=[a-z0-9._]{5,20}$)(?=.*[a-z]+.*)(?!.*[_.]{2})[^_.].*[^_.]$", ErrorMessage = "Only a-z 0-9 . _ characters allowed")]
        public string TeamId { get; set; }
        public List<TeamMember> TeamMembers { get; set; }

        [Required(ErrorMessage = "Location Required")]
        public string Location { get; set; }

        public ModalMessage Message { get; set; }
    }

    public class TeamMember
    {
        [Required(ErrorMessage = "Field Required"), StringLength(60, ErrorMessage = "Character Limit Exceeded")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }
    }

    public class ModalMessage
    {
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
