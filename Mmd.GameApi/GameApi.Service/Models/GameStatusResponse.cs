using System;
using System.Collections.Generic;
using DataAccess.Model;

namespace GameApi.Service.Models
{
    public class GameStatusResponse : ApiResponse
    {
        public new GameStatusData Data { get; set; }
    }

    public class GameStatusData : ApiResponseData
    {
        public int SecretLength { get; set; }
        public List<TeamData> Participants { get; set; }

    }
}