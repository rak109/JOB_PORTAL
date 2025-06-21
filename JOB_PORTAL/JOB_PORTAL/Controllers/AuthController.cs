using JOB_PORTAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace JOB_PORTAL.Controllers
{
    public class AuthController : Controller
    {
        JOB_PORTALContext entities = new JOB_PORTALContext();
        Random random = new Random();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            
         
                byte[] password = System.Text.Encoding.UTF8.GetBytes(form["Password"]);
                string username = form["Username"].ToString();
                var result  = (from t in entities.Users
                               where t.Username == username && t.Password==password
                               select t).FirstOrDefault();
                
                if (result != null)
                {
                    HttpContext.Session.SetString("userId", result.UserId.ToString());
                    return RedirectToAction("Index", "JobSeeker");
                }
                else
                {
                    ViewBag.error = "Invalid Credentials";
                }
          
            return View();
        }


        [HttpGet]
        public IActionResult RegisterSeeker()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterSeeker(IFormCollection form) 
        {
            int userId = random.Next();
            User user = new User()
            {
                UserId = userId,
                Username = form["username"],
                Password = System.Text.Encoding.UTF8.GetBytes(form["password"]),
                UserProfile = null,
                UserType = "JOB SEEKER",
                EmailId = form["emailId"]
            };
            try
            {
                var userRes = entities.Users.Add(user);
               
                JobSeeker jobSeeker = new JobSeeker()
                {
                    JobSeekerId = random.Next(),
                    UserId = userId,
                };

                var jobSeekerRes = entities.JobSeekers.Add(jobSeeker);

                int i = entities.SaveChanges();

                return RedirectToAction("Index", "Auth");
            }

            catch (Exception e)
            {
                ViewBag.error = "Error While Creating Account Please Try Again!";
                return View();
            }
        }


    }
}
