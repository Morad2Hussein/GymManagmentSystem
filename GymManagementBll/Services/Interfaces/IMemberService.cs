using GymManagementBll.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.Services.Interfaces
{
    internal interface IMemberService
    {

        IEnumerable<MemberViewModel> GetALLMembers();
        bool CreateMember(CreateMemberViewModel createMember);
        MemberViewModel? GetMemberDetails(int MemberId);
        HealthRecordViewModel? GetMemberHealthRecord(int MemberId);
        MemberUpdateViewModel? GetMemberUpdateDetails(int MemberId);
             // New Update data =>  
        bool UpdateMemberDetails(int id, MemberUpdateViewModel memberUpdateDetails);
      bool RenewMember(int MemberId);
    }
}
