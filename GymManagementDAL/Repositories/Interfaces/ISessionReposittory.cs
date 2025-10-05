using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ISessionRepository
    {
        // GetAll
        IEnumerable<Session> GetAll();
        // GET BY ID 
        Session? GetbyId(int id);
        // ADD
        int Add(Session session);
        // Update 
        int Update(Session session);
        // Delete
        int Delete(int id);

    }
}
