using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Hacathoon_Master.DAL
{
    public class UserDAL
    {
        private readonly IConfiguration _configuration;

        public UserDAL(IConfiguration configuration)
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

        [Authorize(Roles = "ADMIN")]
        public User GetUser(int userId)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                return db.User.Where(u => u.Id == userId).SingleOrDefault();
            }
        }

        public void CreateUser(RegisterViewModel registerViewModel)
        {
            var user = new User()
            {
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                Email = registerViewModel.Email,
                RegistrationDate = DateTime.Now,
                Phone = registerViewModel.Phone,
                DateOfBirth = DateTime.Now,
                RoleId = (int)Roles.USER
            };
            CreateUser(user, registerViewModel.Password);
        }

        [Authorize(Roles = "ADMIN")]
        public void CreateUser(UserViewModel userViewModel)
        {
            var user = new User()
            {
                Name = userViewModel.Name,
                Surname = userViewModel.Surname,
                Email = userViewModel.Email,
                RegistrationDate = DateTime.Now,
                Phone = userViewModel.Phone,
                DateOfBirth = userViewModel.DateOfBirth,
                RoleId = userViewModel.RoleId
            };
            CreateUser(user, userViewModel.Password);
        }

        public void CreateUser(User user, string password)
        {
            try
            {
                var md5 = MD5.Create();
                var hash = Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(password)));

                var user_auth = new User_Auth()
                {
                    UserLogin = user.Email,
                    UserPassword = hash
                };

                using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
                {
                    db.User.Add(user);
                    db.User_Auth.Add(user_auth);
                    db.SaveChanges();
                }
            }
            catch (MySqlException)
            {
                return;
            }
        }

        public void EditUser(User user)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.User.Update(user);
                db.SaveChanges();
            }
        }
    }
}