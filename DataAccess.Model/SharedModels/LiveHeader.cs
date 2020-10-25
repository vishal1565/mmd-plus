using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model.SharedModels
{
    public class LiveHeader
    {
        public LiveHeader()
        {
            GameId = null;
            RoundId = null;
            RoundNumber = null;
            Phase = null;
            SecretLength = null;
        }

        public Guid? GameId { get; set; }
        public Guid? RoundId { get; set; }
        public long? RoundNumber { get; set; }
        public string Phase { get; set; }
        public int? SecretLength { get; set; }
        public long JoiningDuration { get; set; }
        public long RunningDuration { get; set; }
        public long FinishedDuration { get; set; }
        public DateTime PhaseStartTime { get; set; }

    }
}
