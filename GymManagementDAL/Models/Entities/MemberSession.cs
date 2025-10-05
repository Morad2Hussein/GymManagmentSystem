using GymManagementDAL.Models.Common;

namespace GymManagementDAL.Models.Entities
{
    public class MemberSession : BaseEntity
    {
        #region Properties
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public Session Session { get; set; } = null!;
        public int SessionId { get; set; }

        // Note : The BookingDate AS a CreateAt In BaseEntity 
        public bool IsActive { get; set; }
        #endregion
    }
}
