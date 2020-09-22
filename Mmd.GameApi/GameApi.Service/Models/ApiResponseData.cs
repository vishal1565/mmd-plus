using System;
using DataAccess.Model;

namespace GameApi.Service.Models
{
    public abstract class ApiResponseData
    {
        public Guid GameId { get; set; }
        public Guid RoundId { get; set; }
        public int RoundNumber { get; set; }
        public PhaseType RoundPhase { get; set; }
    }
}