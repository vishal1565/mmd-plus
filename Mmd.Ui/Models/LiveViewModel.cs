using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model.SharedModels;

namespace mmd_plus.Models
{
    public class LiveViewModel
    {
        public Guid? GameId { get; set; }
        public Guid? RoundId { get; set; }
        public long? RoundNumber { get; set; }
        public string Status { get; set; }
        public int? SecretLength { get; set; }
        public List<LiveParticipantDetails> Participants_CurrentScore { get; set; }
        public List<LiveParticipantDetails> Participants_TotalScore { get; set; }

    }

}
