using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Microsoft.AspNetCore.Http;
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

        public Hackathon GetHackathon(int hackathonId)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                return db.Hackathons.Where(h => h.Id == hackathonId).SingleOrDefault();
            }
        }

        public void CreateHackathon(Hackathon hackathon)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.Hackathons.Add(hackathon);
                db.SaveChanges();
            }
        }

        public void EditHackathon(Hackathon hackathon)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.Hackathons.Update(hackathon);
                db.SaveChanges();
            }
        }
        
        public byte[] ReadIFormFileToByteArray(IFormFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}