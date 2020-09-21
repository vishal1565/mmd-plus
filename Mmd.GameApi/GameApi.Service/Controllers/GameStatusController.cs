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

        public GameStatusController(IGameApiService service)
        {
            _service = service ?? throw new ArgumentNullException("IGameApiService");
        }
        public async Task<ActionResult<GameStatusResponse>> Index()
        {
            var response = new GameStatusResponse();
            response.RequestId = Guid.NewGuid();
            //var currentStatus = _service.GetCurrentStatus();
            return Ok(response);
        }
    }
}