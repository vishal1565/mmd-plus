using System;
using System.Collections.Generic;
using DataAccess.Model;

namespace GameApi.Service.Models
{
    public class GameStatusResponseData : ApiResponseData
    {
        public int SecretLength { get; set; }
        public List<TeamData> Participants { get; set; }

    }
}