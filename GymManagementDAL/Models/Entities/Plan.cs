
using GymManagementDAL.Models.Common;

namespace GymManagementDAL.Models.Entities
{
    public class Plan :BaseEntity
    {
        #region Properties
        public string Name { get; set; } = null!;
         public string Descrition { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal Pirce { get; set; }
        public bool IsVctive  { get; set; }
        #endregion
        #region Plan - MemberShip 
        // Plan M => 1 MemberShip
        public ICollection<MemberShip> MemberShips { get; set; } = null!;
        #endregion
    }
}
