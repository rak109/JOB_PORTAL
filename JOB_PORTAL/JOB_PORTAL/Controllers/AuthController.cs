using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTAL.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
