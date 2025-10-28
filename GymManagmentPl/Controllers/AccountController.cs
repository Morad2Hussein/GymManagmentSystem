using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.AccountViewModel;
using GymManagementDAL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPl.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }
        #region LogIn
        // login 
        public ActionResult LogIn()
        {
            return View();

        }
        [HttpPost]
        public async Task<ActionResult> Login(LogInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _accountService.ValidateUser(model);
            if (user is null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "Your Account Is Not Allowed");

            if (result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your Account Is Locked Out");

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View(model);
        }
        #endregion
        #region LogOut
        [HttpPost]
        public ActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }

        #endregion
        #region AccessDenied
        public ActionResult AccessDenied() { 
          return View();
        }
        #endregion
    }

}
