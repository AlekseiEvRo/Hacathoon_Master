using System;
using System.Linq;
using System.Security.Claims;
using Hacathoon_Master.DAL;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{
    public class HackathonController : Controller
    {
        private readonly IConfiguration _configuration;

        public HackathonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index(int hackathonId)
        {
            var hackathonDataController = new HackathonDAL(_configuration);
            var entriesDataController = new EntriesDAL(_configuration);
            
            var hackathon = hackathonDataController.GetHackathon(hackathonId);
            var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault());
            var isUserInvolved = entriesDataController.IsUserInvolved(userId, hackathonId);
            
            var hackathonDescriptionViewmodel = new HackathonDescriptionViewModel()
            {
                HackathonId = hackathon.Id,
                Name = hackathon.Name,
                StartDate = hackathon.StartDate,
                EndDate = hackathon.EndDate,
                EndRegistrationDate = hackathon.EndRegistrationDate,
                Type = hackathon.Type,
                Organisation = hackathon.Organisation,
                Image = hackathon.Image,
                Goal = hackathon.Goal,
                Prize = hackathon.Prize,
                TargetAudience = hackathon.TargetAudience,
                IsUserInvolved = isUserInvolved
            };
            return View(hackathonDescriptionViewmodel);
        }

        [HttpGet, Authorize]
        public IActionResult CreateEntry(int hackathonId)
        {
            var entryDal = new EntriesDAL(_configuration);
            var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault());
            entryDal.CreateEntry(userId, hackathonId);
            return RedirectToAction("Index", new {hackathonId = hackathonId});
        }
    }
}