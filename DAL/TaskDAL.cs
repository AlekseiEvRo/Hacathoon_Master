using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Helpers;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.DAL
{
    public class TaskDAL : IFileHelper
    {
        private readonly IConfiguration _configuration;

        public TaskDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<HackathonTask> GetTaskList(int hackathonId)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                return db.HackathonTask.Where(ht => ht.HackathonId == hackathonId).ToList();
            }
        }

        public void CreateTask(HackathonTask task)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.HackathonTask.Add(new HackathonTask()
                {
                    HackathonId = task.HackathonId,
                    Text = task.Text,
                    AnswerTypeId = task.AnswerTypeId,
                    Image = task.Image
                });
                db.SaveChanges();
            }
        }

        public HackathonTask GetTask(int taskId)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                return db.HackathonTask.Where(ht => ht.Id == taskId).SingleOrDefault();
            }
        }

        public void UpdateTask(HackathonTask task)
        {
            using (ApplicationContext db = new ApplicationContext(_configuration["ConnectionString"]))
            {
                db.HackathonTask.Update(task);
                db.SaveChanges();
            }
        }
    }
}