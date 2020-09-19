using GameApi.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Service.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GameStatusController : ControllerBase
    {
        public ActionResult<GameStatusResponse> Index()
        {
            return new GameStatusResponse();
        }
    }
}