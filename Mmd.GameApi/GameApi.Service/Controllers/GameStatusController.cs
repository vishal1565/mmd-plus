using System;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using GameApi.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GameStatusController : Controller
    {
        private readonly RequestContext requestContext;
        private readonly IGameApiService gameApiService;

        public GameStatusController(RequestContext requestContext)
        {
            this.requestContext = requestContext ?? throw new ArgumentNullException("RequestContext");
            //this.gameApiService = gameApiService ?? throw new ArgumentNullException("IGameApiService");
        }

        [HttpGet]
        public async Task<ActionResult<GameStatusResponse>> Get()
        {
            var response = new GameStatusResponse();
            response.RequestId = requestContext.RequestId;
            response.Data = new GameStatusResponseData();
            return Json(response);
        }
    }
}