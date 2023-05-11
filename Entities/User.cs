using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hacathoon_Master.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Column("Registration_Date")]
        public DateTime RegistrationDate { get; set; }
        public string Phone { get; set; }
        [Column("Date_Of_Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        [Column("Role_ID")]
        public int RoleId { get; set; }
    }
}
