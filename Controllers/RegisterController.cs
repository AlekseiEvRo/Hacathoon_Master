using Microsoft.AspNetCore.Mvc;

namespace Hacathoon_Master.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return Index();
        }
    }
}
