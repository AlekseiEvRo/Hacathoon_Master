using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hacathoon_Master.Entities
{
    public class User_Auth
    {  
        [Key]
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public string User_Password { get; set; }
    }
}
