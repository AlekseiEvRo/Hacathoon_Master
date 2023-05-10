using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hacathoon_Master.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {
             using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
             {
                var user_auth = db.User_Auth.Where(auth => auth.User_Login == loginViewModel.Login).FirstOrDefault();
                var md5 = MD5.Create();
                var hash = Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(loginViewModel.Password)));

                if (user_auth.User_Password == hash)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user_auth.User_Login),
                        new Claim(ClaimTypes.Name, db.User.Where(u => u.Email == user_auth.User_Login).FirstOrDefault().Name),
                        new Claim(ClaimTypes.Role, GetUserRole(user_auth.User_Login))
                    };

                    var claim = new ClaimsIdentity(claims, "ApplicationCookie",
                        ClaimsIdentity.DefaultRoleClaimType, ClaimsIdentity.DefaultRoleClaimType);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claim));
                }
                    
                return RedirectToAction("Index", "Home");
            }
           
        }

        private string GetUserRole(string user_login)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                var user_role = db.User.Where(u => u.Email == user_login).FirstOrDefault().Role_ID;

                return db.User_Roles.Where(r => r.Role_ID == user_role).FirstOrDefault().Role_Name;
            }
        }
    }
}
