using GameApi.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Service.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GameStatusController : ControllerBase
    {
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<GameStatusResponse> Index()
        {
            return new GameStatusResponse();
        }
    }
}