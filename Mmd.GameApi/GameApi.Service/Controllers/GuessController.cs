using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameApi.Service.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuth")]
    [Produces("application/json")]
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<JsonResult> Post([FromBody] GuessRequestBody body)
        {
            JsonResult response;
            try
            {
                GuessResponse guessResponse = new GuessResponse();

                // Validation Check
                // Throttling Check
                // PreProcess Guess
                // Apply Guess

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

            try
            {
                await requestLoggingService.RecordRequest(RequestMethod.POST, response.StatusCode, RequestApi.Join);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to record request to database, {ex}");
            }

            return response;
        }
    }
}
