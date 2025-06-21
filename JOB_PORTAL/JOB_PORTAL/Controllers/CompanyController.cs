using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using JOB_PORTAL.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JOB_PORTAL.Controllers
{
    public class CompanyController : Controller
    {
        private readonly JOB_PORTALContext _context;
        private readonly IConfiguration _configuration;

        // Temporary in-memory store for OTPs
        private static Dictionary<string, string> otpStore = new Dictionary<string, string>();

        public CompanyController(JOB_PORTALContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginCompany(CompanyLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = _context.Companies.FirstOrDefault(c => c.EmailId == model.EmailId);
                if (company != null && PasswordHelper.Verify(model.Password, company.Password))
                {
                    return RedirectToAction("CompanyDashboard");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        public IActionResult RegisterCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterCompany(CompanyRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Companies.Any(c => c.EmailId == model.EmailId))
                {
                    ModelState.AddModelError("EmailId", "Email already registered.");
                    return View(model);
                }

                var random = new Random();
                var companyId = random.Next(10000000, 99999999); // 8-digit random ID

                var newCompany = new Company
                {
                    CompanyId = companyId,
                    CompanyName = model.CompanyName,
                    EmailId = model.EmailId,
                    Password = PasswordHelper.Hash(model.Password)
                };

                _context.Companies.Add(newCompany);
                _context.SaveChanges();

                return RedirectToAction("LoginCompany");
            }

            return View(model);
        }

        public IActionResult ForgotPasswordCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordCompany(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var company = _context.Companies.FirstOrDefault(c => c.EmailId == model.EmailId);
            if (company == null)
            {
                ModelState.AddModelError("EmailId", "Email not found.");
                return View(model);
            }

            var random = new Random();
            string otp = random.Next(100000, 999999).ToString();
            otpStore[model.EmailId] = otp;

            try
            {
                await EmailHelper.SendEmailAsync(_configuration, model.EmailId, "Your OTP", $"Your OTP is: {otp}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to send OTP: " + ex.Message);
                return View(model);
            }

            TempData["Email"] = model.EmailId;
            return RedirectToAction("VerifyOtp");
        }

        [HttpGet]
        public IActionResult VerifyOtp()
        {
            string email = TempData["Email"]?.ToString();
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("ForgotPasswordCompany");

            return View(new VerifyOtpViewModel { EmailId = email });
        }

        [HttpPost]
        public IActionResult VerifyOtp(VerifyOtpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!otpStore.ContainsKey(model.EmailId) || otpStore[model.EmailId] != model.Otp)
            {
                ModelState.AddModelError("Otp", "Invalid or expired OTP.");
                return View(model);
            }

            otpStore.Remove(model.EmailId);
            TempData["Email"] = model.EmailId;
            return RedirectToAction("ResetPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            string email = TempData["Email"]?.ToString();
            if (string.IsNullOrEmpty(email)) return RedirectToAction("ForgotPasswordCompany");

            return View(new ResetPasswordViewModel { EmailId = email });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var company = _context.Companies.FirstOrDefault(c => c.EmailId == model.EmailId);
            if (company == null)
            {
                ModelState.AddModelError("", "Email not found.");
                return View(model);
            }

            company.Password = PasswordHelper.Hash(model.NewPassword);
            _context.SaveChanges();

            TempData["SuccessMsg"] = "Password reset successfully! Please login.";
            return RedirectToAction("LoginCompany");
        }

        public IActionResult CompanyDashboard()
        {
            return View();
        }
    }
}
