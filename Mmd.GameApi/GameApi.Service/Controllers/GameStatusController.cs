using System;
using System.Threading.Tasks;
using GameApi.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameStatusController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GameStatusResponse>> Get()
        {
            var response = new GameStatusResponse();
            response.RequestId = Guid.NewGuid();
            response.Data = new GameStatusResponseData();
            return Ok(response);
        }
    }
}