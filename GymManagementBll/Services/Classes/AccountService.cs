using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.AccountViewModel;
using GymManagementDAL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(UserManager<ApplicationUser> userManager) {
          _userManager = userManager;
        }
        
        public ApplicationUser? ValidateUser(LogInViewModel logInViewModel)
        {
            var User = _userManager.FindByEmailAsync(logInViewModel.Email).Result;
            if (User is  null)  return null;
            var IsPasswordValid = _userManager.CheckPasswordAsync(User , logInViewModel.Password).Result;
           return IsPasswordValid ? User : null;

        }
    }
}
