using System.ComponentModel.DataAnnotations;

namespace JOB_PORTAL
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress]
        public string EmailId { get; set; }
    }

    public class VerifyOtpViewModel
    {
        [Required]
        public string EmailId { get; set; }

        [Required]
        [Display(Name = "OTP")]
        public string Otp { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        public string EmailId { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, Compare("NewPassword"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
