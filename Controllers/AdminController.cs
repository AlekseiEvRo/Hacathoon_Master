using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hacathoon_Master.AppContext;
using Hacathoon_Master.DAL;
using Hacathoon_Master.Entities;
using Hacathoon_Master.Helpers;
using Hacathoon_Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hacathoon_Master.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IFileHelper _fileHelper;

        public AdminController(IConfiguration configuration, IFileHelper fileHelper)
        {
            _configuration = configuration;
            _fileHelper = fileHelper;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Users()
        {
            var userController = new UserDAL(_configuration);
            var userListViewModel = new UserListViewModel()
            {
                Users = userController.GetUsers()
            };
            return View(userListViewModel);
        }

        public IActionResult Hackathons()
        {
            var hackathonController = new HackathonDAL(_configuration);
            var hackathonViewModel = new HackathonListViewModel()
            {
                Hackathons = hackathonController.GetHackathonList()
            };
            return View(hackathonViewModel);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateUser(UserViewModel userViewModel)
        {
            var userController = new UserDAL(_configuration);
            userController.CreateUser(userViewModel);
            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult EditUser(int userId)
        {
            var userController = new UserDAL(_configuration);
            var user = userController.GetUser(userId);
            var userViewModel = new UserViewModel()
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                RoleId = user.RoleId
            };
            return View(userViewModel);
        }
        
        [HttpPost]
        public IActionResult EditUser(UserViewModel userViewModel)
        {
            var user = new User()
            {
                Id = userViewModel.UserId,
                Name = userViewModel.Name,
                Surname = userViewModel.Surname,
                Email = userViewModel.Email,
                Phone = userViewModel.Phone,
                DateOfBirth = userViewModel.DateOfBirth,
                RegistrationDate = DateTime.Now,
                RoleId = userViewModel.RoleId
            };
            
            var userController = new UserDAL(_configuration);
            userController.EditUser(user);
            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult CreateHackathon()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateHackathon(HackathonViewModel hackathonViewModel)
        {
            var hackathonController = new HackathonDAL(_configuration);
            var fileBytes = _fileHelper.ReadIFormFileToByteArray(hackathonViewModel.Image);
            var hackathon = new Hackathon()
            {
                Id = hackathonViewModel.HackathonId,
                Name = hackathonViewModel.Name,
                StartDate = hackathonViewModel.StartDate,
                EndDate = hackathonViewModel.EndDate,
                EndRegistrationDate = hackathonViewModel.EndRegistrationDate,
                Type = hackathonViewModel.Type,
                Organisation = hackathonViewModel.Organisation,
                Image = fileBytes,
                Goal = hackathonViewModel.Goal,
                Prize = hackathonViewModel.Prize,
                TargetAudience = hackathonViewModel.TargetAudience
            };
            hackathonController.CreateHackathon(hackathon);
            return RedirectToAction("Hackathons");
        }
        
        [HttpGet]
        public IActionResult EditHackathon(int hackathonId)
        {
            var hackathonController = new HackathonDAL(_configuration);
            var taskDal = new TaskDAL(_configuration);
            var hackathon = hackathonController.GetHackathon(hackathonId);
            var stream = new MemoryStream(hackathon.Image);
            IFormFile file = new FormFile(stream, 0, hackathon.Image.Length, "name", "fileName");
            var hackathonViewModel = new HackathonViewModel()
            {
                HackathonId = hackathon.Id,
                Name = hackathon.Name,
                StartDate = hackathon.StartDate,
                EndDate = hackathon.EndDate,
                EndRegistrationDate = hackathon.EndRegistrationDate,
                Type = hackathon.Type,
                Organisation = hackathon.Organisation,
                Image = file,
                Goal = hackathon.Goal,
                Prize = hackathon.Prize,
                TargetAudience = hackathon.TargetAudience,
                HackathonTasks = taskDal.GetTaskList(hackathonId),
            };
            return View(hackathonViewModel);
        }
        
        [HttpPost]
        public IActionResult EditHackathon(HackathonViewModel hackathonViewModel)
        {
            var hackathonController = new HackathonDAL(_configuration);
            var fileBytes = new byte[0];
            if (hackathonViewModel.Image != null)
            {
                fileBytes = _fileHelper.ReadIFormFileToByteArray(hackathonViewModel.Image);
            }
            else
            {
                fileBytes = hackathonController.GetHackathon(hackathonViewModel.HackathonId).Image;
            }
            var hackathon = new Hackathon()
            {
                Id = hackathonViewModel.HackathonId,
                Name = hackathonViewModel.Name,
                StartDate = hackathonViewModel.StartDate,
                EndDate = hackathonViewModel.EndDate,
                EndRegistrationDate = hackathonViewModel.EndRegistrationDate,
                Type = hackathonViewModel.Type,
                Organisation = hackathonViewModel.Organisation,
                Image = fileBytes,
                Goal = hackathonViewModel.Goal,
                Prize = hackathonViewModel.Prize,
                TargetAudience = hackathonViewModel.TargetAudience
            };
            hackathonController.EditHackathon(hackathon);
            return RedirectToAction("Hackathons");
        }

        [HttpGet]
        public IActionResult CreateTask(int hackathonId)
        {
            var taskViewModel = new TaskViewModel()
            {
                HackathonId = hackathonId,
            };
            return View(taskViewModel);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskViewModel taskViewModel)
        {
            var taskDal = new TaskDAL(_configuration);

            var task = new HackathonTask()
            {
                HackathonId = taskViewModel.HackathonId,
                Text = taskViewModel.Text,
                AnswerTypeId = taskViewModel.AnswerTypeId,
                Image = _fileHelper.ReadIFormFileToByteArray(taskViewModel.Image)
            };
            
            taskDal.CreateTask(task);
            
            return RedirectToAction("EditHackathon", new { hackathonId = taskViewModel.HackathonId });
        }
        
        [HttpGet]
        public IActionResult EditTask(int taskId)
        {
            var taskDal = new TaskDAL(_configuration);
            var task = taskDal.GetTask(taskId);
            
            var taskViewModel = new TaskViewModel()
            {
                Id = task.Id,
                AnswerTypeId = task.AnswerTypeId,
                HackathonId = task.HackathonId,
                Text = task.Text,
            };
            
            return View(taskViewModel);
        }

        [HttpPost]
        public IActionResult EditTask(TaskViewModel taskViewModel)
        {
            var taskDal = new TaskDAL(_configuration);
            var fileBytes = new byte[0];
            
            if (taskViewModel.Image != null)
            {
                fileBytes = _fileHelper.ReadIFormFileToByteArray(taskViewModel.Image);
            }
            else
            {
                fileBytes = taskDal.GetTask(taskViewModel.Id).Image;
            }

            var task = new HackathonTask()
            {
                AnswerTypeId = taskViewModel.AnswerTypeId,
                HackathonId = taskViewModel.HackathonId,
                Image = fileBytes,
                Id = taskViewModel.Id,
                Text = taskViewModel.Text
            };
            
            taskDal.UpdateTask(task);
            return RedirectToAction("EditHackathon", new { hackathonId = taskViewModel.HackathonId });
        }
    }
}