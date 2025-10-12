using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository :IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionWithTrainerAndCategory();
        int GetCountOfBookedSlotes( int id);
        Session? GetSessionWithTrainerAndCategory(int id);
      
    }
}
