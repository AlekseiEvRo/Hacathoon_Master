using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hacathoon_Master.Entities
{
    public class Entry
    {
        [Key, Column("Hackathon_Id")]
        public int HackathonId { get; set; }
        [Column("User_ID")]
        public int UserId { get; set; }
    }
}