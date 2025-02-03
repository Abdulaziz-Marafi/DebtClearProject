using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class EditViewModel
    {

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required.")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long.")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required.")]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters long.")]
        public string LName { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        [MinLength(6, ErrorMessage = "Email Address must be at least 6 characters long.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public decimal Balance { get; set; }

        public IFormFile? NewImg { get; set; }
        public string Img { get; internal set; }
        public string Id { get; internal set; }
    }
}
