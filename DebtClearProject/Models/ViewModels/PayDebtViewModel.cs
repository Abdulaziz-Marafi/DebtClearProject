using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class PayDebtViewModel
    {
        public Guid UserDebtId { get; set; }

        public Guid DebtId { get; set; }

        [Range(0.01, 10000, ErrorMessage ="Amount Must be positive and between (0.01 - 10000).")]
        public decimal Amount { get; set; }

        [Display(Name = "Remaining Debt")]
        public decimal RemainingDebt { get; set; }

        public string? DebtName { get; set; }
    }
}
