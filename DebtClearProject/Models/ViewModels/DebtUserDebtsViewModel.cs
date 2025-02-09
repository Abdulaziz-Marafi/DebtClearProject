namespace DebtClearProject.Models.ViewModels
{
    public class DebtUserDebtsViewModel
    {
            
        public Guid DebtId { get; set; }
        public Guid UserDebtsId { get; set; }
        public string? DebtName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Ratio { get; set; }
        public string DebtSplit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DebtStatus Status { get; set; }
        public string SharedWithName { get; set; }
        public decimal RemainingDebt { get; set; }
        public bool CanPay { get; set; }
        public bool CanApproveReject { get; set; }

        public bool IsPaid { get; set; }

        public enum DebtStatus
        {
            Approved,
            Pending,
            Rejected,
            Paid
        }


    }
}

