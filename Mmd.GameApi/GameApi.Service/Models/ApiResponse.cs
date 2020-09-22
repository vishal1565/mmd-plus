using System;
using DataAccess.Model.SharedModels;

namespace GameApi.Service.Models
{
    public abstract class ApiResponse
    {
        public Guid RequestId { get; set; }
        public Error Err { get; set; }
        public abstract ApiResponseData Data { get; set; }
    }
}