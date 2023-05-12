using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Hacathoon_Master.Models
{
    public class HackathonViewModel
    {
        public int HackathonId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EndRegistrationDate { get; set; }
        public string Type { get; set; }
        public string Organisation { get; set; }
        public IFormFile Image { get; set; }
        public string Goal { get; set; }
        public string Prize { get; set; }
        public string TargetAudience { get; set; }
    }
}