using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hacathoon_Master.Entities
{
    public class Hackathon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("Start_Date")]
        public DateTime StartDate { get; set; }
        [Column("End_Date")]
        public DateTime EndDate { get; set; }
        [Column("End_Registration_Date")]
        public DateTime EndRegistrationDate { get; set; }
        public string Type { get; set; }
        public string Organisation { get; set; }
        public byte[] Image { get; set; }
        public string Goal { get; set; }
        public string Prize { get; set; }
        [Column("Target_Audience")]
        public string TargetAudience { get; set; }

        public string GetFileFromBytes()
        {
            return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(Image));
        }
    }
}