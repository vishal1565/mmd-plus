using GameApi.Service.Models;
using System.Collections.Generic;

namespace GameApi.Service
{
    public class GuessResponseData : ApiResponseData
    {
        public List<string> Guesses { get; set; }
    }
}