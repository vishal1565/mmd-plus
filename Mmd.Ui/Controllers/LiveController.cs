using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using mmd_plus.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using DataAccess.Data.Abstract;
using DataAccess.Model;
using DataAccess.Model.SharedModels;
using Microsoft.Extensions.Logging;


namespace mmd_plus.Controllers
{
    public class LiveController : Controller
    {
       
        private readonly ILiveService _liveService;

        public LiveController(ILiveService liveService)
        {
            _liveService = liveService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            LiveResponse liveResponse = await _liveService.GetLiveStatus();

            LiveViewModel live = new LiveViewModel
            {
                GameId = liveResponse.GameId,
                RoundId = liveResponse.RoundId,
                RoundNumber = liveResponse.RoundNumber,
                Status = liveResponse.Status,
                SecretLength = liveResponse.SecretLength,
                Participants_CurrentScore = liveResponse.Participants.OrderByDescending(o => o.CurrentScore).ToList(),
                Participants_TotalScore = liveResponse.Participants.OrderByDescending(o => o.TotalScore).ToList()

            };

            return View(live);
      
        }
            
            
        
    }
}
