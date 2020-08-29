using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using DataAccess.Model.SharedModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mmd_plus.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mmd_plus.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegistrationService _service;

        public RegisterController(IRegistrationService service)
        {
            _service = service;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var locations = new List<string> { "Location", "DB-Pune", "DB-Bangalore" };
            ViewData["Location"] = new SelectList(locations, "Location");
            var model = new RegisterViewModel
            {
                TeamId = "",
                TeamMembers = new List<TeamMember> { new TeamMember { EmailId = "" } },
                Location = ""
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(RegisterViewModel request)
        {
            Validate(request);

            if(ModelState.IsValid)
            {
                var validRegisterTeam = PreProcess(request);

                var message = _service.RegisterOrUpdate(validRegisterTeam);

                if (message == null)
                {
                    // return view with error message of "Registration Failed"
                }
                else
                    return View("Index");

            }
            return View();
        }

        private void Validate(RegisterViewModel request)
        {
            //ModelState.AddModelError("TeamId", "Team Already Registered");
            return;
        }

        private RegisterTeam PreProcess(RegisterViewModel request)
        {
            var newTeamMembers = new List<string>();

            foreach (var tm in request.TeamMembers)
                newTeamMembers.Add(tm.EmailId.ToLower());

            return new RegisterTeam
            {
                TeamId = request.TeamId.ToLower(),
                Location = request.Location.ToUpper(),
                RequestTime = DateTime.UtcNow,
                TeamMembers = newTeamMembers
            };
        }
    }
}
