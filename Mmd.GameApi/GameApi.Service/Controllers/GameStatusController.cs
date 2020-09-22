using System;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using DataAccess.Model.SharedModels;
using GameApi.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameApi.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GameStatusController : Controller
    {
        private readonly RequestContext requestContext;
        private readonly IGameApiService gameApiService;
        private readonly ILogger<GameStatusController> logger;

        public GameStatusController(RequestContext requestContext, IGameApiService gameApiService)
        {
            this.requestContext = requestContext ?? throw new ArgumentNullException("RequestContext");
            this.gameApiService = gameApiService ?? throw new ArgumentNullException("IGameApiService");
        }

        [HttpGet]
        public async Task<ActionResult<GameStatusResponse>> Get()
        {
            JsonResult response = null;
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                if (logger != null)
                    logger.LogError($"Error Occurred while fetching current gamestatus, {ex}");

                response = new JsonResult(new GameStatusResponse { 
                    RequestId = requestContext.RequestId,
                    Err = new Error { 
                        Message = "Internal Server Error",
                        Description = "Server Failed to fetch Current Gamestatus"
                    }
                });

                response.StatusCode = 500;
            }
            return response;
        }
    }
}