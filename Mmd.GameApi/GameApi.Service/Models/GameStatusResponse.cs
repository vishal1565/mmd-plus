using System;
using System.Collections.Generic;
using DataAccess.Model;

namespace GameApi.Service.Models
{
    public class GameStatusResponse
    {
        public Guid GameId { get; set; }
        public Guid RoundId { get; set; }
        public PhaseType RoundPhase { get; set; }
        public int RoundNumber { get; set; }
        public List<TeamData> Participants { get; set; }

    }
}