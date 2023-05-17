using Hacathoon_Master.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{
    public class HackathonController : Controller
    {
        private readonly IConfiguration _configuration;

        public HackathonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index(int hackathonId)
        {
            var hackathonDataController = new HackathonDAL(_configuration);
            var hackathon = hackathonDataController.GetHackathon(hackathonId);
            return View(hackathon);
        }
    }
}