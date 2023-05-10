using System.Collections.Generic;
using System.Linq;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize(Roles = "ADMIN")]
        public List<User> GetUsers()
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                return db.User.ToList();
            }
        }
    }
}