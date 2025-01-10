using System.ComponentModel.DataAnnotations;

namespace diveWebMVC.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "帳號")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string? PasswordHash { get; set; }

        //public string Email { get; set; }

    }
}
