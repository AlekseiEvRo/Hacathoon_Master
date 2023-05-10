using System.Linq;
using Hacathoon_Master.AppContext;
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
        
        public ActionResult Users()
        {
            var userController = new UserController(_configuration);
            var userViewModel = new UserViewModel()
            {
                Users = userController.GetUsers()
            };
            return View(userViewModel);
        }
    }
}