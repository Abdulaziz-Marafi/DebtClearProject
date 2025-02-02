using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class LoginViewModel
    {

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        [MinLength(6, ErrorMessage = "Email Address must be at least 6 characters long.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 25 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
