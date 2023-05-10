using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hacathoon_Master.Controllers
{   
    [Authorize(Roles = "USER")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}