using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberShipReposittory
    {
        // GetAll
        IEnumerable<MemberShip> GetAll();
        // GET BY ID 
        MemberShip? GetbyId(int id);
        // ADD
        int Add(MemberShip membership);
        // Update 

        int Update(MemberShip membership);
        // Delete
        int Delete(int id);
    }
}
