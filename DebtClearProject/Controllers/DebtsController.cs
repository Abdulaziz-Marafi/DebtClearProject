using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DebtClearProject.Data;
using DebtClearProject.Models;
using DebtClearProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DebtClearProject.Controllers
{
    public class DebtsController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private ApplicationDbContext db;

        public DebtsController(UserManager<User> _userManager, SignInManager<User> _signInManager, RoleManager<IdentityRole> _roleManager, IWebHostEnvironment _webHostEnvironment, ApplicationDbContext _db)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            webHostEnvironment = _webHostEnvironment;
            db = _db;
        }

        // Create Debts

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DebtViewModel newDebt)
        {
            if (ModelState.IsValid)
            {
              var debt = new Debt
              {
                  //DebtId = Guid.NewGuid(),
                  TotalAmount = newDebt.TotalAmount,
                  //DebtSplit = newDebt.DebtSplit,
                  StartDate = newDebt.StartDate,
                  EndDate = newDebt.EndDate,
                  Status = Debt.DebtStatus.Pending
              };
                db.Debts.Add(debt);
                db.SaveChanges();

                // Parse the DebtSplit string into a list of decimal values
                var curr = await userManager.GetUserAsync(User);
                var selectedUser = await userManager.FindByEmailAsync(newDebt.SelectedEmailUser);
                //var users = _context.Users.ToList(); Get selected User?
                if (selectedUser == null || curr == null)
                {
                    ModelState.AddModelError("SelectedEmailUser", "User not found");
                    return View(newDebt);
                }
                var userDebts = new[]
                {
                    new UserDebts{
                    //UserDebtsId = Guid.NewGuid(),
                    Split = newDebt.Ratios[0] * newDebt.TotalAmount,
                    IsPaid = false,
                    IsAccepted = false,
                    RemainingDebt = newDebt.Ratios[0] * newDebt.TotalAmount,
                    UserId = curr.Id,
                    DebtId = debt.DebtId},

                    new UserDebts{
                    Split = newDebt.Ratios[1] * newDebt.TotalAmount,
                    IsPaid = false,
                    IsAccepted = false,
                    RemainingDebt = newDebt.Ratios[1] * newDebt.TotalAmount,
                    UserId = selectedUser.Id,
                    DebtId = debt.DebtId}
                    
                };
                db.UserDebts.AddRange(userDebts);
                db.SaveChanges();

                //for (int i = 0; i < splits.Length; i++)
                //{
                //    var userDebt = new UserDebts
                //    {
                //        //UserDebtsId = Guid.NewGuid(),
                //        Split = (decimal)splits[i] / totalRatio * newDebt.TotalAmount,
                //        IsPaid = false,
                //        IsAccepted = false,
                //        RemainingDebt = (decimal)splits[i] / totalRatio * newDebt.TotalAmount,
                //        UserId = newDebt.UserId[i],
                //        DebtId = debt.DebtId
                //    };
                //    db.UserDebts.Add(userDebt);                    
                //}
                //db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(newDebt);
            }
        }

        public IActionResult Index()
        {
            var debts = db.Debts.ToList();
            return View(debts);
        }

    }
}
