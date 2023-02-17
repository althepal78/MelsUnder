using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MUC.DataAccess.Data;
using MUC.Models;
using MUC.Models.ViewModels;

namespace MUC.Web.Controllers.Identity
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(ApplicationDbContext db, SignInManager<IdentityUser> signInManager)
        {
            _db = db;
            _signInManager = signInManager;
        }
      
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel logingModel)
        {
            // if the model state is no good let it go back you're wrong
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("LoginModel", "You're missing a email or password");
                return View(logingModel);
            }
            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(l => l.Email == logingModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("User", "User doesn't exists!");
                return View(logingModel);
            }
            var result = await _signInManager.PasswordSignInAsync(logingModel.Email, logingModel.Password,logingModel.RememberMe, lockoutOnFailure: true);
            if (!result.Succeeded)
            {                
                ModelState.AddModelError("User", "Something is not matching up try again");
                return View(logingModel);
            }

            TempData["success"] = "Logged In Successfully";

            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Customer");
        }

    }
}
