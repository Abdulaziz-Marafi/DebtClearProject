namespace DebtClearProject.Models.ViewModels
{
    public class DebtUserDebtsViewModel
    {
        public IEnumerable<Debt> Debts { get; set; }
        public IEnumerable<UserDebts> UserDebts { get; set; }
    }
}
