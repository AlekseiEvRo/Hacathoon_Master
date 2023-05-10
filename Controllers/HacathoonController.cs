using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{
    public class HacathoonController : Controller
    {
        private readonly IConfiguration _configuration;

        public HacathoonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}