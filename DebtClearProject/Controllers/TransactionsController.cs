using DebtClearProject.Data;
using DebtClearProject.Models;
using DebtClearProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DebtClearProject.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext db;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public TransactionsController(ApplicationDbContext _db, UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View(db.Transactions);
        }

        public async Task<IActionResult> UserIndex()
        {
            var curr = await userManager.GetUserAsync(User);
            if (curr == null) { return NotFound(); }

            var transactions = db.Transactions
                .Where(x => x.UserId == curr.Id)
                .Select(x => new TransactionViewModel
                {
                    Amount = x.Amount,
                    Status = (TransactionViewModel.TransactionStatus)x.Status,
                    TransactionDate = x.TransactionDate,
                    RemainingBalance = x.RemainingBalance,
                    DebtName = x.Debt.DebtName 
                }).ToList();

            return View(transactions);
        }


        public async Task<IActionResult> DebtTransactions1(Guid? id)
        {
            if (id == null) { return RedirectToAction("Index2", "Debts"); }
            var debt = await db.Debts.FindAsync(id);
            if (debt == null) { return RedirectToAction("Index2", "Debts"); }
            return View(db.Transactions.Where(x => x.DebtId == debt.DebtId));
        }

        public async Task<IActionResult> DebtTransactions(Guid? id)
        {
            if (id == null) { return RedirectToAction("Index2", "Debts"); }
            var debt = await db.Debts.FindAsync(id);
            if (debt == null) { return RedirectToAction("Index2", "Debts"); }

            var transactions = db.Transactions
                .Where(x => x.DebtId == debt.DebtId)
                .Select(x => new TransactionViewModel
                {
                    Amount = x.Amount,
                    Status = (TransactionViewModel.TransactionStatus)x.Status,
                    TransactionDate = x.TransactionDate,
                    RemainingBalance = x.RemainingBalance,
                    DebtName = debt.DebtName, // Get the debt name
                    SharedDebtorName = db.UserDebts
                        .Where(ud => ud.DebtId == debt.DebtId && ud.UserId == x.UserId)
                        .Select(ud => ud.User.FirstName + " " + ud.User.LastName)
                        .FirstOrDefault() // Name of person who paid
                }).ToList();

            return View(transactions);
        }

    }
}
