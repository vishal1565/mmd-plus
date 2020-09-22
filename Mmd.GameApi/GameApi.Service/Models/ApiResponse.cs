using System;
using DataAccess.Model.SharedModels;
using Newtonsoft.Json;

namespace GameApi.Service.Models
{
    public abstract class ApiResponse<T> where T : ApiResponseData
    {
        public Guid RequestId { get; set; }
        public Error Err { get; set; }
        public abstract T Data { get; set; }
    }

    public class GameStatusResponse : ApiResponse<GameStatusResponseData>
    {
        public override GameStatusResponseData Data { get; set; }
    }

    public class JoinResponse : ApiResponse<JoinResponseData>
    {
        public override JoinResponseData Data { get; set; }
    }

    public class GuessResponse : ApiResponse<GuessResponseData>
    {
        public override GuessResponseData Data { get; set; }
    }
}