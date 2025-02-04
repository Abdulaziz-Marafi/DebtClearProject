using DebtClearProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using DebtClearProject.Models;
using Microsoft.AspNetCore.Hosting;

namespace DebtClearProject.Controllers
{
    public class UserController : Controller
    {

        #region InjectedServices
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserController(UserManager<User> _userManager, SignInManager<User> _signInManager,IWebHostEnvironment _webHostEnvironment)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            webHostEnvironment = _webHostEnvironment;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {

            var r = await userManager.GetUserAsync(User);
            //HttpContext.Session.SetString("Balance", r.Balance.ToString());
            HttpContext.Session.SetString("Name", r.UserName);

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var r = await userManager.GetUserAsync(User);
            if (r == null) { return NotFound(); }

            EditViewModel model = new EditViewModel()
            {
                Balance = r.Balance,
                Email = r.Email,
                FName = r.FirstName,
                LName = r.LastName,
                Img = r.ProfilePicture,
                Id = r.Id
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FName;
                user.LastName = model.LName;
                user.Email = model.Email;

                if (model.NewImg != null)
                {
                    string uniqueFile = UploadFile(model.NewImg);
                    user.ProfilePicture = uniqueFile;
                }
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Display");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

            }
            return View(model);
        }

        public string UploadFile(IFormFile Image)
        {
            string uniqueFileName = null;

            if (Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


    }
}
