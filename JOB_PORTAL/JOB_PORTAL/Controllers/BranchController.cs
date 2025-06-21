using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTAL.Controllers
{
    public class BranchController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddEmployer()
        {
            
            return View();
        }
        [HttpGet]
        public IActionResult DisplayEmployer()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DisplayJob()
        {
            return View();
        }


    }
}
