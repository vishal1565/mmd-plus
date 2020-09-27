using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using GameApi.Service.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameApi.Service.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = "BasicAuth")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Throttle]
    public class GuessController : Controller
    {
        private readonly RequestContext requestContext;
        private readonly IGameApiService gameApiService;
        private readonly IRequestLoggingService requestLoggingService;
        private readonly ILogger<GuessController> logger;

        public GuessController(RequestContext requestContext, IGameApiService gameApiService, IRequestLoggingService requestLoggingService, ILogger<GuessController> logger)
        {
            this.requestContext = requestContext ?? throw new ArgumentNullException("RequestContext");
            this.gameApiService = gameApiService ?? throw new ArgumentNullException("GameApiService");
            this.requestLoggingService = requestLoggingService ?? throw new ArgumentNullException("RequestLoggingService");
            this.logger = logger ?? throw new ArgumentNullException("Logger");
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return new JsonResult(new GuessResponse());
        }

        [HttpPost]
        public async Task<JsonResult> Post([FromBody] GuessRequestBody requestBody)
        {
            JsonResult response;

            try
            {
                GuessResponse guessResponse = new GuessResponse();

                var guesses = requestBody.Guesses;

                // PreProcess here

                var body = PreProcess(requestBody);

                guessResponse.RequestId = requestContext.RequestId;

                response = new JsonResult(guessResponse)
                {
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                logger.LogError($"Error Occurred while fetching current gamestatus");

                response = new JsonResult(new GuessResponse
                {
                    RequestId = requestContext.RequestId,
                    Err = new Error
                    {
                        Message = "Internal Server Error",
                        Description = "Server Failed to fetch Current Gamestatus"
                    }
                })
                {
                    StatusCode = 500
                };
            }

            return response;
        }

        private GuessRequestBody PreProcess(GuessRequestBody requestBody)
        {
            var newModel = new GuessRequestBody();

            newModel.Guesses = new List<SingleGuessRequestObject>();

            foreach(var guess in requestBody.Guesses)
            {
                newModel.Guesses.Add(new SingleGuessRequestObject
                {
                    Team = guess.Team.ToLowerInvariant(),
                    Guess = guess.Guess.ToLowerInvariant()
                });
            }

            return newModel;
        }
    }
}
