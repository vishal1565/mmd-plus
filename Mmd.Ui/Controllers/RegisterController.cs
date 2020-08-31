using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data.Abstract;
using DataAccess.Model.SharedModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using mmd_plus.Models;
using Newtonsoft.Json;

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


        void PopulateViewData(RegisterViewModel request)
        {
            var locations = _service.GetLocations();
            ViewData["Location"] = new SelectList(locations, "Location");
            var list = new List<string>();
            foreach (var tm in request.TeamMembers)
                list.Add(tm.EmailId);
            ViewBag.CurrentIds = JsonConvert.SerializeObject(list);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = new RegisterViewModel
            {
                TeamId = "",
                TeamMembers = new List<TeamMember> { new TeamMember { EmailId = "" } },
                Location = "",
                Message = new ModalMessage { Type = "", Message = "" }
            };
            PopulateViewData(model);
            return View(model);
        }

        public IActionResult LoadData(int draw, int start, int length, IList<Dictionary<string, string>> columns, IList<Dictionary<string, string>> order)
        {
            var columnRequest = new List<string>();

            Dictionary<string, string> searchValues = new Dictionary<string, string>();

            for(int i = 0;i < columns.Count; i++)
            {
                string key = "columns[" + i.ToString() + "][{0}]";
                string dataKey = String.Format(key, "data");
                string search = String.Format(key, "search");
                string searchValueKey = search + "[value]";

                Request.Query.TryGetValue(dataKey, out StringValues col);
                Request.Query.TryGetValue(searchValueKey, out StringValues searchValue);

                columnRequest.Add(col);

                if (!string.IsNullOrWhiteSpace(searchValue))
                    searchValues.Add(col, searchValue);
            }

            Int32.TryParse(order[0]["column"], out int index);

            string sortColumn = columnRequest[index];
            string sortDir = order[0]["dir"];

            var data = _service.GetRegisteredTeams(searchValues, sortColumn, sortDir, start, length, out int filteredCount, out int totalCount);

            return Json(new { draw, recordsFiltered = filteredCount, recordsTotal = totalCount, data });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(RegisterViewModel request)
        {
            Validate(request);

            PopulateViewData(request);

            if(ModelState.IsValid)
            {
                var validRegisterTeam = PreProcess(request);

                var message = _service.RegisterOrUpdate(validRegisterTeam);

                var responseModel = new RegisterViewModel
                {
                    TeamId = "",
                    TeamMembers = new List<TeamMember> { new TeamMember { EmailId = "" } },
                    Location = "",
                    Message = new ModalMessage()
                };

                if (message == null)
                    responseModel.Message.Type = "error";
                else
                    responseModel.Message.Type = "success";

                responseModel.Message.Message = message;
                return View(responseModel);

            }
            request.Message = new ModalMessage { Type = "", Message = "" };
            return View(request);
        }

        private void Validate(RegisterViewModel request)
        {
            if (!_service.TeamIdIsUnique(request.TeamId))
                ModelState.AddModelError("TeamId", "TeamId already exists");
            for (var i = 0;i < request.TeamMembers.Count;i++)
                if (!_service.EmailIdIsUnique(request.TeamMembers[i].EmailId))
                    ModelState.AddModelError($"TeamMembers[{i}].EmailId", "User already registered");
            if (request.Location == "Select")
                ModelState.AddModelError("Location", "Specify a Location");
            return;
        }

        private RegisterTeam PreProcess(RegisterViewModel request)
        {
            var newTeamMembers = new List<string>();

            foreach (var tm in request.TeamMembers)
                newTeamMembers.Add(tm.EmailId.ToLowerInvariant());

            return new RegisterTeam
            {
                TeamId = request.TeamId.ToLowerInvariant(),
                Location = request.Location,
                RequestTime = DateTime.UtcNow,
                TeamMembers = newTeamMembers
            };
        }
    }
}
