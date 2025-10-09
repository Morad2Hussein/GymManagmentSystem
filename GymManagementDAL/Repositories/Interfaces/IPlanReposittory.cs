using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IPlanReposittory
    {
        // GetAll
        IEnumerable<Plan> GetAll();
        // GET BY ID 
        Plan? GetbyId(int id);
        // ADD
        int Add(Plan plan);
        // Update 
        int Update(Plan plan) ;
        // Delete
        int Delete(int id);

    }
}
