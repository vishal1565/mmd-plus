using System;
using System.Collections.Generic;
using DataAccess.Model;

namespace GameApi.Service.Models
{
    public class GameStatusResponse
    {
        public Error Err { get; set; }
        public Data Data { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Description { get; set; }
    }

    public class Data
    {
        public Guid GameId { get; set; }
        public Guid RoundId { get; set; }
        public PhaseType RoundPhase { get; set; }
        public int RoundNumber { get; set; }
        public int SecretLength { get; set; }
        public List<TeamData> Participants { get; set; }

    }
}