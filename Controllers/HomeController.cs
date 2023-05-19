using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hacathoon_Master.DAL;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var hackathonController = new HackathonDAL(_configuration);
            var hackathonList = hackathonController.GetHackathonList();
            var homeViewModel = new HomeViewModel()
            {
                HackathonList = hackathonList.Take(3).ToList()
            };
            return View(homeViewModel);
        }
        
        public IActionResult AllHackathons()
        {
            var hackathonDal = new HackathonDAL(_configuration);
            var hackathonList = hackathonDal.GetHackathonList();

            return View(hackathonList);
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult About()
        {
            return View();
        }
    }
}