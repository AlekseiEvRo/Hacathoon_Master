using System;
using System.Linq;
using System.Security.Claims;
using Hacathoon_Master.DAL;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{   
    [Authorize(Roles = "USER")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult EditProfile(UserViewModel userViewModel)
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
                RoleId = (int)Roles.USER //TODO оставляю константно, возможно нужно будет подтягивать роль (на данный момент только пользователь может это делать)
            };
            
            var userController = new UserDAL(_configuration);
            userController.EditUser(user);
            return RedirectToAction("EditProfile");
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            var userDal = new UserDAL(_configuration);
            var user = userDal.GetUser(Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault()));
            var userViewModel = new UserViewModel()
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth
            };
            return View(userViewModel);
        }
    }
}