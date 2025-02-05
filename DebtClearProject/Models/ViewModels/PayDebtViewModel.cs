using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class PayDebtViewModel
    {
        public Guid UserDebtId { get; set; }

        [Range(0.01, 10000, ErrorMessage ="Amount Must be positive and below 10000.")]
        public decimal Amount { get; set; }

        public decimal RemainingDebt { get; set; }
    }
}
