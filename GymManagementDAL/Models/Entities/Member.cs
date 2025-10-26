using GymManagementDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models.Entities
{
    public class Member : GymUser
    {
        #region  Properties
        public string Photo { get; set; } = null!;
        // Note the JionDate is a CreateAT 
        #endregion
        #region Relationships 
        #region Member - HealthRecord 
        // 1 => 1 relationship
        public HealthRecord HealthRecord { get; set; } = null!;

        #endregion
        #region  Member - MemberShip 
        // member M => 1 MemberShip 
        public ICollection<MemberShip> memberShips { get; set; } = null!;
        #endregion
        #region Member - MemberSession
        // Member has M => MemberSession 
        public ICollection<MemberSession> MemberSessions { get; set; } = null!; 
        #endregion
        #endregion
    }
}
