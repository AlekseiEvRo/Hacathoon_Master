using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime Registration_Date { get; set; }
        public string Phone { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Email { get; set; }
        public int Role_ID { get; set; }
    }
}
