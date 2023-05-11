using System;
using System.Linq;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authorization;
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
            var userController = new UserController(_configuration);
            var userListViewModel = new UserListViewModel()
            {
                Users = userController.GetUsers()
            };
            return View(userListViewModel);
        }

        public IActionResult Hackathons()
        {
            var hackathonController = new HackathonController(_configuration);
            var hackathonViewModel = new HackathonsViewModel()
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
            var userController = new UserController(_configuration);
            userController.CreateUser(userViewModel);
            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult EditUser(int userId)
        {
            var userController = new UserController(_configuration);
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
            
            var userController = new UserController(_configuration);
            userController.EditUser(user);
            return RedirectToAction("Users");
        }
    }
}