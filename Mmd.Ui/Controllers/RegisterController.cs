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

        void PopulateRegisterViewData(RegisterViewModel request)
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
            PopulateRegisterViewData(model);
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

            PopulateRegisterViewData(request);

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
                ModelState.Clear();
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
                if (!string.IsNullOrWhiteSpace(request.TeamMembers[i].EmailId) && !_service.EmailIdIsUnique(request.TeamMembers[i].EmailId))
                    ModelState.AddModelError($"TeamMembers[{i}].EmailId", "User already registered");
            if (request.Location == "Select")
                ModelState.AddModelError("Location", "Specify a Location");
            return;
        }

        private RegisterTeam PreProcess(RegisterViewModel request)
        {
            var newTeamMembers = new List<string>();

            foreach (var tm in request.TeamMembers)
                if(!string.IsNullOrWhiteSpace(tm.EmailId))
                    newTeamMembers.Add(tm.EmailId.ToLowerInvariant());

            return new RegisterTeam
            {
                TeamId = request.TeamId.ToLowerInvariant(),
                Location = request.Location,
                RequestTime = DateTime.UtcNow,
                TeamMembers = newTeamMembers
            };
        }

        public IActionResult ModifyTeam()
        {
            var model = new ModifyTeamViewModel
            {
                TeamId = "",
                SecretToken = ""
            };
            PopulateModifyTeamViewBag(model, false);
            return View(model);
        }

        public void PopulateModifyTeamViewBag(ModifyTeamViewModel request, bool validTeam)
        {
            ViewBag.ValidTeam = validTeam;
            var list = new List<string>();
            if(request.TeamMembers != null)
            foreach (var tm in request.TeamMembers)
                list.Add(tm.EmailId);
            ViewBag.CurrentIds = JsonConvert.SerializeObject(list);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ModifyTeam(ModifyTeamViewModel request)
        {
            if (!string.IsNullOrWhiteSpace(request.TeamId))
                request.TeamId = request.TeamId.ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(request.SecretToken))
                request.SecretToken = request.SecretToken.ToLowerInvariant();

            if (!_service.DoesTeamExist(request.TeamId))
                ModelState.AddModelError("TeamId", "Team Doesn't Exist");

            else if (!_service.IsSecretTokenCorrect(request.TeamId, request.SecretToken))
                ModelState.AddModelError("SecretToken", "Incorrect Token");
            
            if(ModelState.IsValid)
            {
                var teamMembers = new List<TeamMember>();
                _service.GetTeamMembers(request.TeamId).ForEach(tm => teamMembers.Add(new TeamMember { EmailId = tm }));
                request.TeamMembers = teamMembers;
                PopulateModifyTeamViewBag(request, true);
                return View(request);
            }

            PopulateModifyTeamViewBag(request, false);
            return View(request);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ChangeTeamMembers(ModifyTeamViewModel request)
        {
            var userList = request.TeamMembers.Select(tm => tm.EmailId.ToLowerInvariant()).ToList();

            for(var i = 0;i < userList.Count;i++)
                if (!_service.EmailIdIsUnique(userList[i], request.TeamId.ToLowerInvariant()))
                    ModelState.AddModelError($"TeamMembers[{i}].EmailId", "User Already Registered");
            
            if (ModelState.IsValid)
            {
                // Change the teamMembers
                bool successFul = _service.UpdateTeamMembers(request.TeamId.ToLowerInvariant(), userList);

                var model = new ModifyTeamViewModel
                {
                    TeamId = "",
                    SecretToken = ""
                };

                ModelState.Clear();
                PopulateModifyTeamViewBag(model, false);
                if (successFul)
                {
                    model.Message = new ModalMessage { Type = "success", Message = "Team Change SucessFul" };
                    return View("ModifyTeam", model);
                }
                else
                {
                    model.Message = new ModalMessage { Type = "error", Message = "Change Failed" };
                    return View("ModifyTeam", model);
                }
            }
            
            PopulateModifyTeamViewBag(request, true);
            return View("ModifyTeam", request);
        }
    }
}
