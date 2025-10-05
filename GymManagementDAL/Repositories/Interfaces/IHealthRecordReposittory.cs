using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IHealthRecordReposittory
    {
        /// Get All
        IEnumerable<HealthRecord> GetAll();
        // GetByID
        HealthRecord? GetById (int id);
        // Add 
        int Add(HealthRecord healthRecord);
        // update 
        int Update(HealthRecord healthRecord);
        //DELETE 
        int Delete(int id);
    }
}
