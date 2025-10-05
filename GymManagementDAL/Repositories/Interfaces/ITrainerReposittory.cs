using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ITrainerReposittory
    {
        // GetAll
        IEnumerable<Trainer> GetAll();
        // GET BY ID 
        Trainer? GetbyId(int id);
        // ADD
        int Add(Trainer trainer);
        // Update 
        int Update(Trainer trainer );
        // Delete
        int Delete(int id);
    }
}
