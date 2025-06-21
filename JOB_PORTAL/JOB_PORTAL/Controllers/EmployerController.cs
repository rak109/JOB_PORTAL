using JOB_PORTAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTAL.Controllers
{
    public class EmployerController : Controller
    {
        JOB_PORTALContext ob = new JOB_PORTALContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddJob()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddJob(IFormCollection f)
        {
            DateTime postingDateTime = DateTime.Parse(f["Posting_Date"]);
            DateTime LastDateTime = DateTime.Parse(f["Last_Date"]);
            Job job = new Job()
            {
                JobId = Random.Shared.Next(),
                Title = f["Title"],
                Description = f["Description"],
                PostingDate = DateOnly.FromDateTime(postingDateTime),
                LastDate = DateOnly.FromDateTime(LastDateTime),
                Location = f["Location"],
                JobType = f["JobType"],
                SkillId = 1,
                Domain = f["Domain"],
                Salary = Convert.ToInt32(f["Salary"]),
                ExperienceLevel = f["Experience_Level"],
                EmployerId = 1
            };
            ob.Jobs.Add(job);
            ob.SaveChanges();

            return View();
        }

        public IActionResult DisplayJob()
        {
            return View();
        }
        public IActionResult ViewJobSeeker()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
