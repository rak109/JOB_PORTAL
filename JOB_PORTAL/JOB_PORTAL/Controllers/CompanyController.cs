using JOB_PORTAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTAL.Controllers
{
    public class CompanyController : Controller
    {
        JOB_PORTALContext obj = new JOB_PORTALContext();
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
