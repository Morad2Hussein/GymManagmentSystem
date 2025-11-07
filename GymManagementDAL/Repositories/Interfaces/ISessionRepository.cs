using GymManagementDAL.Models.Entities;


namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository :IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionWithTrainerAndCategory();
        int GetCountOfBookedSlotes( int id);
        Session? GetSessionWithTrainerAndCategory(int id);
      
    }
}
