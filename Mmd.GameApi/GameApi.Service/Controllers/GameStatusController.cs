using System;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using GameApi.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Service.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GameStatusController : ControllerBase
    {
        private readonly IGameApiService _service;
        private readonly RequestContext _requestContext;

        public GameStatusController(IGameApiService service, RequestContext requestContext)
        {
            _service = service ?? throw new ArgumentNullException("IGameApiService");
            _requestContext = requestContext ?? throw new ArgumentNullException("RequestContext");
        }
        public async Task<ActionResult<GameStatusResponse>> Index()
        {
            var response = new GameStatusResponse();
            response.RequestId = _requestContext.RequestId;
            //var currentStatus = _service.GetCurrentStatus();
            return Ok(response);
        }
    }
}