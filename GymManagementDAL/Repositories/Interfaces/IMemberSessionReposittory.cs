using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberSessionReposittory
    {
        /// Get All
        IEnumerable<MemberSession> GetAll();
        // GetByID
        MemberSession? GetById(int id);
        // Add 
        int Add(MemberSession memberSession);
        // update 
        int Update(MemberSession memberSession);
        //DELETE 
        int Delete(int id);
    }
}
