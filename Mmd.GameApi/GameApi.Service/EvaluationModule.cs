using DataAccess.Data.Abstract;
using GameApi.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApi.Service
{
    public class EvaluationModule
    {
        private readonly IGameApiService _gameApiService;

        public EvaluationModule(IGameApiService gameApiService)
        {
            _gameApiService = gameApiService ?? throw new ArgumentNullException("gameApiService");
        }

        public async Task<EvaluationResult> EvaluateTheGuess(string targetTeam, string guess)
        {
            var result = new EvaluationResult();

            // fetch existing secret code

            return result;
        }
    }
}
