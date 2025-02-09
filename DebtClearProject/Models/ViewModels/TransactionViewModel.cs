using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class TransactionViewModel
    {

        public decimal Amount { get; set; }
        public TransactionStatus Status { get; set; }

        [Display(Name =" Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = " Remaining Balance")]
        public decimal RemainingBalance { get; set; }

        public string? SharedDebtorName { get; set; }

        [Display(Name = "Debt Name")]
        public string? DebtName { get; set; }

        public enum TransactionStatus
        {
            Success,
            Failed
        }
    }
}
