using GymManagementBll.ViewModels.MemberSessionViewModel;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.Services.Interfaces
{
    public interface IMemberSessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        IEnumerable<SessionViewModel> GetMembersForUpcomingBySessionId(int sessionId);
        IEnumerable<SessionViewModel> GetMembersForOngoingBySessionId(int sessionId);
        IEnumerable<SessionViewModel> GetMembersForDropDown(int sessionId);
        bool CancelBooking(int MemberId, int SessionId);
        bool CreateNewBooking(CreateMemberSessionViewModel createdBooking);
        bool MemberAttended(int MemberId, int SessionId);
    }
}
