using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class TransactionViewModel
    {


        public Guid TransactionId { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter the amount")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

      

        public string Type { get; set; }

        public string? ReceiverId { get; set; }
    }
}
