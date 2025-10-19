using GymManagementBll.ViewModels.MembershipViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.Services.Interfaces
{
    public  interface IMembershipService
    {
        IEnumerable<MemberShipViewModel> GetAllMemberShips();
        bool CreateMembership(CreateMemberShipViewModel CreatedMemberShip);
        bool DeleteMemberShip(int MemberId);
        IEnumerable<PlanSelectListViewModel> GetPlansForDropDown();
        IEnumerable<MemberSelectListViewModel> GetMembersForDropDown();
    }
}
