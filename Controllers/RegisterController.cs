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
            var user = new User()
            {
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                Email = registerViewModel.Email,
                Phone = registerViewModel.Phone,
                Date_Of_Birth = DateTime.Now,
                Registration_Date = DateTime.Now,
                Role_ID = (int)Roles.USER
            };

            var userId = 0;
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.User.Add(user);
                db.SaveChanges();
                userId = db.User.Where(u => u.Email == user.Email).FirstOrDefault().Id;
            }
            
            var md5 = MD5.Create();
            var hash = Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(registerViewModel.Password)));

            var user_auth = new User_Auth()
            {
                User_ID = userId,
                User_Login = user.Email,
                User_Password = hash
            };
            
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.User_Auth.Add(user_auth);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
