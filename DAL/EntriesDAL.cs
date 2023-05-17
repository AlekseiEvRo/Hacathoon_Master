using System.Linq;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.DAL
{
    public class EntriesDAL
    {
        private readonly IConfiguration _configuration;

        public EntriesDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize(Roles = "USER")]
        public void CreateEntry(int userId, int hackathonId)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.Entries.Add(new Entry()
                {
                    HackathonId = hackathonId,
                    UserId = userId
                });
                db.SaveChanges();
            }
        }
        
        [Authorize(Roles = "USER")]
        public bool IsUserInvolved(int userId, int hackathonId)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                return db.Entries.Any(e => e.HackathonId == hackathonId && e.UserId == userId);
            }
        }
    }
}