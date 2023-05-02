using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hacathoon_Master.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Phone { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public string Email { get; set; }
        public int Role_ID { get; set; }
    }
}
