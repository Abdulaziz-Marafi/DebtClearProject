using System.ComponentModel.DataAnnotations.Schema;

namespace DebtClearProject.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus Status { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal RemainingBalance { get; set; }



        // Foreign Key 1
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User? User { get; set; }

        // Foreign Key 2
        [ForeignKey("DebtId")]
        public Guid DebtId { get; set; }
        public Debt? Debt { get; set; }

        public enum TransactionStatus
        {
            Success,
            Failed
        }
    }
}
