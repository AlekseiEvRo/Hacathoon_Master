using Hacathoon_Master.Entities;
using Microsoft.AspNetCore.Http;

namespace Hacathoon_Master.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public int HackathonId { get; set; }
        public string Text { get; set; }
        public IFormFile Image { get; set; }
        public int AnswerTypeId { get; set; }
    }
}