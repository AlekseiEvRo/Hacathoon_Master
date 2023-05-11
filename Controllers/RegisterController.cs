using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IConfiguration _configuration;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            var userController = new UserController(_configuration);
            userController.CreateUser(registerViewModel);
            return RedirectToAction("Index", "Home");
        }
    }
}
