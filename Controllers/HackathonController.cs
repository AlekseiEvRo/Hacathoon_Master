using System.Collections.Generic;
using System.Linq;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
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

        public List<Hackathon> GetHackathonList()
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                return db.Hackathons.ToList();
            }
        }
    }
}