using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hacathoon_Master.Entities
{
    public class User_Roles
    {
        [Key, Column("Role_ID")]
        public int RoleId { get; set; }
        [Column("Role_Name")]
        public string RoleName { get; set; }
    }
}