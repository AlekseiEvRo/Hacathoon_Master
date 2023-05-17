using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hacathoon_Master.Entities
{
    [Table("hackathon_Task")]
    public class HackathonTask
    {
        [Key, Column("ID")]
        public int Id { get; set; }
        [Column("hackathon_ID")]
        public int HackathonId { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        [Column("answer_Type_ID")]
        public int AnswerTypeId { get; set; }
    }
}