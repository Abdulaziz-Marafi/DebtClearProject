using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class ViewProfileViewModel
    {
        [Required(ErrorMessage = "Please Enter Your First Name")]
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "Your First Name Must Be At Least 2 Charachters Long")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        [MinLength(2, ErrorMessage = "Your Last Name Must Be At Least 2 Charachters Long")]
        public string LName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        [MinLength(6, ErrorMessage = "Email Address must be at least 6 characters long.")]
        public string Email { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public string? Img { get; set; }

        [Required]
        public string Id { get; set; }
    }
}
