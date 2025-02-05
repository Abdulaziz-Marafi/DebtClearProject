using DebtClearProject.Data;
using DebtClearProject.Models;
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
            return View(db.Transactions.Where(x => x.UserId == curr.Id));
        }

        public async Task<IActionResult> DebtTransactions(Guid? id)
        {
            if (id == null) { return RedirectToAction("Index2", "Debts"); }
            var debt = await db.Debts.FindAsync(id);
            if (debt == null) { return RedirectToAction("Index2", "Debts"); }
            return View(db.Transactions.Where(x => x.DebtId == debt.DebtId));
        }
    }
}
