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
                string debtSplit = (newDebt.Ratios[0]).ToString() + " : "+(newDebt.Ratios[1]).ToString();
                var debt = new Debt
              {
                  //DebtId = Guid.NewGuid(),
                  TotalAmount = newDebt.TotalAmount,
                  DebtSplit = debtSplit,
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
                if(curr.Id == selectedUser.Id)
                {
                    ModelState.AddModelError("SelectedEmailUser", "You cannot owe yourself");
                    return View(newDebt);
                }
                var userDebts = new[]
                {
                    new UserDebts{
                    //UserDebtsId = Guid.NewGuid(),
                    Split = (newDebt.Ratios[0])/100,
                    IsPaid = false,
                    Status = UserDebts.DebtStatus.Approved,
                    RemainingDebt = (newDebt.Ratios[0] * newDebt.TotalAmount)/100,
                    UserId = curr.Id,
                    DebtId = debt.DebtId},

                    new UserDebts{
                    Split = (newDebt.Ratios[1])/100,
                    IsPaid = false,
                    Status = UserDebts.DebtStatus.Pending,
                    RemainingDebt = (newDebt.Ratios[1] * newDebt.TotalAmount)/100,
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

        public async Task<IActionResult> Index2()
        {
            var curr = await userManager.GetUserAsync(User);
            var debts = db.UserDebts.Where(x=> x.UserId==curr.Id).ToList();
            return View(debts);
        }

        [HttpGet]
        public async Task<IActionResult> DebtPayment(Guid id)
        {
            var curr = await userManager.GetUserAsync(User);

            var userDebt = db.UserDebts.Where(x => x.UserDebtsId == id).FirstOrDefault();
            // Add ViewModel
            PayDebtViewModel debt = new PayDebtViewModel()
            {
                UserDebtId = userDebt.UserDebtsId,
                RemainingDebt = userDebt.RemainingDebt,
                Amount = 0.1m
            };
            return View(debt);
        }

        [HttpPost]
        public async Task<IActionResult> DebtPayment(PayDebtViewModel debt)
        {
            // Add ViewModel
            if (ModelState.IsValid)
            {
                var curr = await userManager.GetUserAsync(User);
                if (curr == null)
                {
                    return NotFound();
                }
                var userDebt = db.UserDebts.Where(x => x.UserDebtsId == debt.UserDebtId).FirstOrDefault();
                if (userDebt == null)
                {
                    return NotFound();
                }
                // If user wants to pay more than the balance they currently have
                if (debt.Amount > curr.Balance)
                {
                    ModelState.AddModelError("Amount", "Amount is greater than balance");
                    Transaction tran = new Transaction()
                    {
                        Amount = debt.Amount,
                        TransactionDate = DateTime.Now,
                        Status = Transaction.TransactionStatus.Failed,
                        RemainingBalance = curr.Balance,
                        UserId = curr.Id,
                        DebtId = userDebt.DebtId

                    };
                    db.Transactions.Add(tran);
                    db.SaveChanges();
                    return View(debt);
                }
                // If user wants to pay more than the remaining debt
                if (debt.Amount > userDebt.RemainingDebt && curr.Balance > debt.Amount)
                {
                    curr.Balance -= userDebt.RemainingDebt;
                   
                    Transaction tran = new Transaction()
                    {
                        Amount = userDebt.RemainingDebt,
                        TransactionDate = DateTime.Now,
                        Status = Transaction.TransactionStatus.Success,
                        RemainingBalance = curr.Balance,
                        UserId = curr.Id,
                        DebtId = userDebt.DebtId

                    };
                    userDebt.RemainingDebt = 0;
                    userDebt.IsPaid = true;
                    db.Transactions.Add(tran);
                    db.SaveChanges();
                }
                else
                {
                    userDebt.RemainingDebt -= debt.Amount;
                    curr.Balance -= debt.Amount;

                    Transaction tran = new Transaction()
                    {
                        Amount = debt.Amount,
                        TransactionDate = DateTime.Now,
                        Status = Transaction.TransactionStatus.Success,
                        RemainingBalance = curr.Balance,
                        UserId = curr.Id,
                        DebtId = userDebt.DebtId

                    };
                    db.Transactions.Add(tran);
                    // If no remaining debt then its paid off
                    if(userDebt.RemainingDebt == 0)
                    {
                        userDebt.IsPaid = true;
                    }
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index2));
                }
            }
            return View(debt);
        }

        public async Task<IActionResult> ApproveDebt(Guid? id)
        {
            var curr = await userManager.GetUserAsync(User);
            var userDebt = db.UserDebts.Where(x => x.UserDebtsId == id).FirstOrDefault();
            if (userDebt == null)
            {
                return NotFound();
            }
            userDebt.Status = UserDebts.DebtStatus.Approved;
            db.SaveChanges();
            return RedirectToAction(nameof(Index2));
        }
        public async Task<IActionResult> RejecrDebt(Guid? id)
        {
            var curr = await userManager.GetUserAsync(User);
            var userDebt = db.UserDebts.Where(x => x.UserDebtsId == id).FirstOrDefault();
            if (userDebt == null)
            {
                return NotFound();
            }
            userDebt.Status = UserDebts.DebtStatus.Rejected;
            db.SaveChanges();
            return RedirectToAction(nameof(Index2));
        }

    }
}
