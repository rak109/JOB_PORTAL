using JOB_PORTAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTAL.Controllers
{
    public class CompanyDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        JOB_PORTALContext entity = new JOB_PORTALContext();

        [HttpGet]
        public IActionResult CreateBranch(int companyId)
        {
            // Pass companyId to the view via ViewBag
            ViewBag.CompanyId = companyId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateBranch(IFormCollection form)
        {
           
            try
            {
                var branch = new Branch
                {
                    BranchId = int.Parse(form["BranchId"]),
                    CompanyId = Convert.ToInt32(form["CompanyId"]),
                    BranchName = form["BranchName"],
                    Location = form["Location"],
                    Password = System.Text.Encoding.UTF8.GetBytes(form["Password"])
                };

                entity.Branches.Add(branch);
                entity.SaveChanges();

                return RedirectToAction("DisplayBranch");
            }
            catch(Exception e)
            {
                ViewBag.error = "Something Went Wrong";
                return View();
            }
        }

        [HttpGet]
        public IActionResult DisplayBranch()
        {
            try
            {
                var allBranches = entity.Branches.ToList();
                return View(allBranches);
            }
            catch (Exception e)
            {
                ViewBag.error = "Something Went Wrong";
                return View(new List<Branch>());
            }
        }


        [HttpGet]
        public IActionResult EditBranch(int id)
        {
            var branch = entity.Branches.FirstOrDefault(b => b.BranchId == id);
            if (branch == null) return NotFound();
            return View(branch);
        }

        [HttpPost]
        public IActionResult EditBranch(Branch model)
        {
            var branch = entity.Branches.FirstOrDefault(b => b.BranchId == model.BranchId);
            if (branch == null) return NotFound();

            branch.BranchName = model.BranchName;
            branch.Location = model.Location;
            // Update other fields as needed

            entity.SaveChanges();
            return RedirectToAction("DisplayBranch");
        }

        [HttpPost]
        public IActionResult DeleteBranch(int id)
        {
            var branch = entity.Branches.FirstOrDefault(b => b.BranchId == id);
            if (branch != null)
            {
                entity.Branches.Remove(branch);
                entity.SaveChanges();
            }
            return RedirectToAction("DisplayBranch");
        }



        [HttpGet]
        public IActionResult Jobs()
        {
            return View();
        }
    }
}
