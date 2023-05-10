using System.ComponentModel.DataAnnotations;

namespace Hacathoon_Master.Entities
{
    public class User_Roles
    {
        [Key]
        public int Role_ID { get; set; }
        public string Role_Name { get; set; }
    }
}