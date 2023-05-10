using Hacathoon_Master.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hacathoon_Master.AppContext
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<User_Auth> User_Auth { get; set; } = null!;
        public DbSet<User_Roles> User_Roles { get; set; } = null!;

        private readonly string _connectionString;

        public ApplicationContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString,
                ServerVersion.AutoDetect(_connectionString));
        }
    }
}
