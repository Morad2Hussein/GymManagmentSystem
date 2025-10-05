using GymManagementDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Models.Entities
{
    public class HealthRecord :BaseEntity
    {
        #region Properties
        public decimal Heigth { get; set; }
        public decimal Weigth { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }
        // note that LastUpdae as a UpdateAt in GymUser

        #endregion
    }
}
