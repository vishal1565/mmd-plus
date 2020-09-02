using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mmd_plus.Models
{
    public class ModifyTeamViewModel
    {
        [Required(ErrorMessage = "TeamId Required"), StringLength(20, ErrorMessage = "Atleast 5 and max 20 characters allowed", MinimumLength = 5)]
        [RegularExpression("^(?=[a-z0-9._]{5,20}$)(?=.*[a-z]+.*)(?!.*[_.]{2})[^_.].*[^_.]$", ErrorMessage = "Only a-z 0-9 . _ characters allowed")]
        public string TeamId { get; set; }
        [Required(ErrorMessage = "Token Required"), StringLength(32, ErrorMessage = "Incorrect Length", MinimumLength = 32)]
        public string SecretToken { get; set; }
        public List<TeamMember> TeamMembers {get; set;}
        public ModalMessage Message { get; set; }
    }
}
