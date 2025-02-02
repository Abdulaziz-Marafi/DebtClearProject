namespace DebtClearProject.Models
{
    public class Debt
    {
        public Guid DebtId { get; set; }

        public decimal TotalAmount { get; set; }

        public string DebtSplit { get; set; }

        public DateTime StartDate{ get; set; }
        public DateTime EndDate{ get; set; }

        public DebtStatus Status { get; set; }



        public enum DebtStatus
        {
            Approved,
            Pending,
            Rejected
        }
    }
}
