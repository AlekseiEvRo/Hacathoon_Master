using System;
using System.IO;
using System.Linq;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.DAL;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Users()
        {
            var userController = new UserDAL(_configuration);
            var userListViewModel = new UserListViewModel()
            {
                Users = userController.GetUsers()
            };
            return View(userListViewModel);
        }

        public IActionResult Hackathons()
        {
            var hackathonController = new HackathonDAL(_configuration);
            var hackathonViewModel = new HackathonListViewModel()
            {
                Hackathons = hackathonController.GetHackathonList()
            };
            return View(hackathonViewModel);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateUser(UserViewModel userViewModel)
        {
            var userController = new UserDAL(_configuration);
            userController.CreateUser(userViewModel);
            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult EditUser(int userId)
        {
            var userController = new UserDAL(_configuration);
            var user = userController.GetUser(userId);
            var userViewModel = new UserViewModel()
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                RoleId = user.RoleId
            };
            return View(userViewModel);
        }
        
        [HttpPost]
        public IActionResult EditUser(UserViewModel userViewModel)
        {
            var user = new User()
            {
                Id = userViewModel.UserId,
                Name = userViewModel.Name,
                Surname = userViewModel.Surname,
                Email = userViewModel.Email,
                Phone = userViewModel.Phone,
                DateOfBirth = userViewModel.DateOfBirth,
                RegistrationDate = DateTime.Now,
                RoleId = userViewModel.RoleId
            };
            
            var userController = new UserDAL(_configuration);
            userController.EditUser(user);
            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult CreateHackathon()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateHackathon(HackathonViewModel hackathonViewModel)
        {
            var hackathonController = new HackathonDAL(_configuration);
            var fileBytes = hackathonController.ReadIFormFileToByteArray(hackathonViewModel.Image);
            var hackathon = new Hackathon()
            {
                Id = hackathonViewModel.HackathonId,
                Name = hackathonViewModel.Name,
                StartDate = hackathonViewModel.StartDate,
                EndDate = hackathonViewModel.EndDate,
                EndRegistrationDate = hackathonViewModel.EndRegistrationDate,
                Type = hackathonViewModel.Type,
                Organisation = hackathonViewModel.Organisation,
                Image = fileBytes,
                Goal = hackathonViewModel.Goal,
                Prize = hackathonViewModel.Prize,
                TargetAudience = hackathonViewModel.TargetAudience
            };
            hackathonController.CreateHackathon(hackathon);
            return RedirectToAction("Hackathons");
        }
        
        [HttpGet]
        public IActionResult EditHackathon(int hackathonId)
        {
            var hackathonController = new HackathonDAL(_configuration);
            var hackathon = hackathonController.GetHackathon(hackathonId);
            var stream = new MemoryStream(hackathon.Image);
            IFormFile file = new FormFile(stream, 0, hackathon.Image.Length, "name", "fileName");
            var hackathonViewModel = new HackathonViewModel()
            {
                HackathonId = hackathon.Id,
                Name = hackathon.Name,
                StartDate = hackathon.StartDate,
                EndDate = hackathon.EndDate,
                EndRegistrationDate = hackathon.EndRegistrationDate,
                Type = hackathon.Type,
                Organisation = hackathon.Organisation,
                Image = file,
                Goal = hackathon.Goal,
                Prize = hackathon.Prize,
                TargetAudience = hackathon.TargetAudience,
                HackathonTasks = hackathonController.GetHackathonTasks(hackathonId),
            };
            return View(hackathonViewModel);
        }
        
        [HttpPost]
        public IActionResult EditHackathon(HackathonViewModel hackathonViewModel)
        {
            var hackathonController = new HackathonDAL(_configuration);
            var fileBytes = new byte[0];
            if (hackathonViewModel.Image != null)
            {
                fileBytes = hackathonController.ReadIFormFileToByteArray(hackathonViewModel.Image);
            }
            else
            {
                fileBytes = hackathonController.GetHackathon(hackathonViewModel.HackathonId).Image;
            }
            var hackathon = new Hackathon()
            {
                Id = hackathonViewModel.HackathonId,
                Name = hackathonViewModel.Name,
                StartDate = hackathonViewModel.StartDate,
                EndDate = hackathonViewModel.EndDate,
                EndRegistrationDate = hackathonViewModel.EndRegistrationDate,
                Type = hackathonViewModel.Type,
                Organisation = hackathonViewModel.Organisation,
                Image = fileBytes,
                Goal = hackathonViewModel.Goal,
                Prize = hackathonViewModel.Prize,
                TargetAudience = hackathonViewModel.TargetAudience
            };
            hackathonController.EditHackathon(hackathon);
            return RedirectToAction("Hackathons");
        }

        [HttpGet]
        public IActionResult CreateTask(int hackathonId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateTask()
        {
            return Ok();
        }
        
        [HttpGet]
        public IActionResult EditTask(int hackathonId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult EditTask()
        {
            return Ok();
        }
    }
}