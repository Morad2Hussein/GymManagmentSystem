using GymManagementDAL.Models.Common;
using GymManagementDAL.Models.Enum;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models.Entities
{
    public class Trainer : GymUser
    {
        #region Properties
        public Specialties Specialties { get; set; }
        // note HiringDate  as A CreateAt in User GymUsr
        #endregion
        #region Relationships 
        #region  Trainer - Session 
        //  many to one relationship 
        public ICollection<Session> Sessions { get; set; } = null!;
        #endregion
        #endregion
    }
}
