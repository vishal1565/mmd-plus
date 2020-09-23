using System.Collections.Generic;

namespace DataAccess.Model.SharedModels
{
    public class GuessResponseData : ApiResponseData
    {
        public List<string> Guesses { get; set; }
    }
}