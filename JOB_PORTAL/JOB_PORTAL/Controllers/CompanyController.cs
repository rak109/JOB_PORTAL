using Microsoft.AspNetCore.Mvc;
using JOB_PORTAL.Models;
namespace JOB_PORTAL.Controllers
{
    public class CompanyController : Controller
    {
        JOB_PORTALContext ob = new JOB_PORTALContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BranchLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BranchLogin(IFormCollection f)
        {
            try
            {


                int BranchID = Convert.ToInt32(f["Branch_ID"]);



                HttpContext.Session.SetInt32("BranchId", BranchID); //storing branch ID


               


                var password = (f["Password"].ToString());
                var pwd = (from t in ob.Branches where t.BranchId == BranchID select t.Password).FirstOrDefault();

                var res = ob.Branches
                 .FirstOrDefault(t => t.BranchId == BranchID && t.Password == pwd);


                if (res != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ViewBag.Error = "Invalid Credentails";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }



    }
}


