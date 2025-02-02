using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required.")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required.")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long.")]
        public string LastName { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        [MinLength(6, ErrorMessage = "Email Address must be at least 6 characters long.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Confirm Email Address")]
        [Required(ErrorMessage = "Email Confirmation is Required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Confirm Email Address.")]
        [MinLength(6, ErrorMessage = "Email Address must be at least 6 characters long.")]
        [DataType(DataType.EmailAddress)]
        [Compare("Email", ErrorMessage = "Email and Confirm Email must match.")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 25 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Confirm Password must be between 3 and 25 characters")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        public decimal Balance { get; set; }

        public IFormFile? Image { get; set; }



    }
}
