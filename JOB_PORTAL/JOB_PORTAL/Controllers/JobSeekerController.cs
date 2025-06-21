using JOB_PORTAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTAL.Controllers
{
    public class JobSeekerController : Controller
    {
        JOB_PORTALContext entities = new JOB_PORTALContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyJobs()
        {
            return View();
        }

        public IActionResult Profile()
        {
            int userId  = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var res = entities.Users.Find(userId);
            return View(res);
        }

        public IActionResult Resume()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
