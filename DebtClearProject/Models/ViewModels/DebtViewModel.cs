using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class DebtViewModel
    {
       // public Guid DebtId { get; set; }

        public decimal TotalAmount { get; set; }

        //[DebtSplitValidation]
        //public string DebtSplit { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Display(Name = "Debt Name")]
        [Required(ErrorMessage = "Debt Name is Required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Debt Name Must Be Between 3-30 Characters Long")]
        public string DebtName { get; set; }

        //public DebtStatus Status { get; set; }

        //public string[] UserId { get; set; }
        public string SelectedEmailUser { get; set; } // Email of the selected user


        public decimal[] Ratios { get; set; } = new decimal[2]; // Array to store ratios


        public enum DebtStatus
        {
            Approved,
            Pending,
            Rejected
        }
    }
}
