using GymManagementDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models.Entities
{
    public class MemberShip : BaseEntity
    {
        #region  Properties

        public Member Member { get; set; } = null!;
        public int MemberId { get; set; }
        public Plan Plan { get; set; } = null!;

        public int PlanId { get; set; }
        // StartDate AS A CreateAT from BaseEntity
        public DateTime EndDate { get; set; }
        // to check if the plan from member is Active or Expired
        public string Status
        {
            get
            {
                if (EndDate >= DateTime.Now)
                    return "Expired";
                else
                    return "Active";


            }
        }
        #endregion


    }
}
