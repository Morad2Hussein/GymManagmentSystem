using GymManagementBll.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.Services.Interfaces
{
    public interface IPlanServiec
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int id);
        UpdatePlanViewModel? UpdatePlanById(int  Planid);
        bool UpdatePlan(int id, UpdatePlanViewModel planViewModel);
        bool ToggleStatus(int id);

    }
}




