using Microsoft.AspNetCore.Mvc;

namespace DebtClearProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
