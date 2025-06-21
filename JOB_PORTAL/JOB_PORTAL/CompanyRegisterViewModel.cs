using System.ComponentModel.DataAnnotations;

namespace JOB_PORTAL
{
    public class CompanyRegisterViewModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required, EmailAddress]
        public string EmailId { get; set; }

        [Required, MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Compare("Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class CompanyLoginViewModel
    {
        [Required, EmailAddress]
        public string EmailId { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

}

