using GymManagementDAL.Data.Context;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymManagementDbContext _dbContext;
        public SessionRepository(GymManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return _dbContext.Sessions
                                     .Include(x => x.SessionTrainer)
                                     .Include(x => x.SessionCategory)
                                      .ToList();

        }

        public int GetCountOfBookedSlotes(int id)
        {
            return _dbContext.MemberSessions.Count(x => x.SessionId == id);

        }

        public Session? GetSessionWithTrainerAndCategory(int id)
        {
            return _dbContext.Sessions.Include(s => s.SessionTrainer)
                                      .Include(s => s.SessionCategory)
                                      .FirstOrDefault(x => x.Id == id);
        }
    }
}
