using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mmd_plus.Models
{
    public class ModifyTeamViewModel
    {
        [Required(ErrorMessage = "TeamId Required")]
        public string TeamId { get; set; }
        [Required(ErrorMessage = "Token Required"), StringLength(32, ErrorMessage = "Incorrect Length", MinimumLength = 32)]
        public string SecretToken { get; set; }
        public List<TeamMember> TeamMembers {get; set;}
        public ModalMessage Message { get; set; }
    }
}
