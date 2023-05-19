using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.DAL
{
    public class HackathonDAL : IFileHelper
    {
        private readonly IConfiguration _configuration;

        public HackathonDAL(IConfiguration configuration)
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

        public void DeleteHackathon(int hackathonId)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.Hackathons.Remove(db.Hackathons.Where(h => h.Id == hackathonId).SingleOrDefault());
                db.SaveChanges();
            }
        }
    }
}