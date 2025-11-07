using GymManagementDAL.Models.Common;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IMembershipRepository MembershipRepository { get; }
        public ISessionRepository SessionRepository { get; }
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
        int SaveChanges();
    }
}
