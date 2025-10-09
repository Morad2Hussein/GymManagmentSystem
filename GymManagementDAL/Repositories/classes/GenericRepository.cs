using GymManagementDAL.Data.Context;
using GymManagementDAL.Models.Common;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private GymManagementDbContext _dbContext;
        public GenericRepository(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(TEntity entity)=>_dbContext.Set<TEntity>().Add(entity);
         

        public IEnumerable<TEntity> GetAll() => _dbContext.Set<TEntity>().AsNoTracking().ToList();
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
           if (condition is null )
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
           else
                return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }

        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        public void Update(TEntity entity)=>   _dbContext.Set<TEntity>().Update(entity);
         
        public void Delete(TEntity entity)=>   _dbContext.Set<TEntity>().Remove(entity);
         

    }
}
