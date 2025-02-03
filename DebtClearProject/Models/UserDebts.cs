using System.ComponentModel.DataAnnotations.Schema;

namespace DebtClearProject.Models
{
    public class UserDebts
    {
        public Guid UserDebtsId { get; set; }
        public decimal Split { get; set; }
        //public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; }

        public bool IsAccepted { get; set; }

        // Remaining debt to be paid by the user
        public decimal RemainingDebt { get; set; }

        //FK 1 Users
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User? User { get; set; }

        //FK 2 Debt
        [ForeignKey("Debt")]
        public Guid DebtId { get; set; }
        public Debt? Debt { get; set; }
    }
}
