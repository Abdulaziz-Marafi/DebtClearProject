using System.ComponentModel.DataAnnotations;

namespace DebtClearProject.Models.ViewModels
{
    public class TransactionSearchViewModel
    {
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        public decimal? Amount { get; set; }

        public List<TransactionViewModel>? Transactions { get; set; }
    
     }
}
