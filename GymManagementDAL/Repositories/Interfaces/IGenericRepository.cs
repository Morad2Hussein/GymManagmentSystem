using GymManagementDAL.Models.Common;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        // GetAll
        IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null);
        //GetByID
        TEntity? GetById(int id);
        // ADD 
        void Add(TEntity entity);
        // Update
        void Update(TEntity entity);
        // Delete 
        void Delete(TEntity entity);
        bool Exists(Func<TEntity, bool> predicate);





    }
}