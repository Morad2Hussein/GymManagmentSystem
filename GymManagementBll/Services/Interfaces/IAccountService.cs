using GymManagementBll.ViewModels.AccountViewModel;
using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.Services.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidateUser(LogInViewModel logInViewModel);
    }
}
