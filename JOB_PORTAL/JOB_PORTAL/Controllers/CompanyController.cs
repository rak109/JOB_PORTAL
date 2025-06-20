using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTAL.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
