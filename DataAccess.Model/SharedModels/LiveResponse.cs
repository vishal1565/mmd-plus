using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Model;
using Newtonsoft.Json;

namespace DataAccess.Model.SharedModels
{
    public class LiveResponse
    {
        public LiveResponse()
        {
            GameId = null;
            RoundId = null;
            RoundNumber = null;
            Status = null;
            SecretLength = null;
        }

        public Guid? GameId { get; set; }
        public Guid? RoundId { get; set; }
        public long? RoundNumber { get; set; }
        public string Status { get; set; }
        public int? SecretLength { get; set; }
        public List<LiveParticipantDetails> Participants { get; set; }
    }
}
