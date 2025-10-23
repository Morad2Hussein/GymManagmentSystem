using GymManagementBll.ViewModels.MemberViewModels;


namespace GymManagementBll.Services.Interfaces
{
    public interface IMemberService
    {

        IEnumerable<MemberViewModel> GetALLMembers();
        bool CreateMember(CreateMemberViewModel createMember);
        MemberViewModel? GetMemberDetails(int MemberId);
        HealthRecordViewModel? GetMemberHealthRecord(int MemberId);
        MemberUpdateViewModel? GetMemberUpdateDetails(int MemberId);
             // New Update data =>  
        bool UpdateMemberDetails(int id, MemberUpdateViewModel memberUpdateDetails);
      bool RemoveMember(int MemberId);
    }
}


