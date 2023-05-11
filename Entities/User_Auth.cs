using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hacathoon_Master.Entities
{
    public class User_Auth
    {
        [Key,Column("User_Login")]
        public string UserLogin { get; set; }
        [Column("User_Password")]
        public string UserPassword { get; set; }
    }
}
