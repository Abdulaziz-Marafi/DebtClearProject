using DebtClearProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace DebtClearProject.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext db;

        public TransactionsController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.Transactions);
        }
    }
}
