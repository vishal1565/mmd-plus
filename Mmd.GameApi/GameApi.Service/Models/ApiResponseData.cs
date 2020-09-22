using System;
using DataAccess.Model;
using Newtonsoft.Json;

namespace GameApi.Service.Models
{
    public abstract class ApiResponseData
    {
        [JsonProperty(Order = 1)]
        public Guid? GameId { get; set; }
        [JsonProperty(Order = 2)]
        public Guid? RoundId { get; set; }
        [JsonProperty(Order = 3)]
        public int RoundNumber { get; set; }
        [JsonProperty(Order = 4)]
        public PhaseType Status { get; set; }
    }
}