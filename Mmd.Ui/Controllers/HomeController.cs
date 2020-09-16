using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Model.SharedModels;
using Microsoft.AspNetCore.Mvc;
using mmd_plus.Models;
using NotificationService;

namespace mmd_plus.Controllers
{
    public class HomeController : Controller
    {
        private INotificationService _nService;

        public HomeController(INotificationService service)
        {
            _nService = service;
        }
        public IActionResult Index()
        {
            _nService.Notify(new NotificationContent());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
