using GymManagementDAL.Data.Context;
using GymManagementDAL.Models.Common;
using GymManagementDAL.Repositories.classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        // to 
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymManagementDbContext _dbContext;

        public UnitOfWork( GymManagementDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);
            if(_repositories.TryGetValue(EntityType , out var Repo))
                return  (IGenericRepository<TEntity> )Repo;
            var NewRepo = new GenericRepository<TEntity>(_dbContext);
            return NewRepo;

        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
