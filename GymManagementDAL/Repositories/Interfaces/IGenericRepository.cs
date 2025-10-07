using GymManagementDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        // GetAll
        IEnumerable<TEntity> GetAll();
        //GetByID
        TEntity? GetById(int id);
        // ADD 
        int Add(TEntity entity);
        // Update
        int Update (TEntity entity);
        // Delete 
        int Delete (TEntity entity);




    }
}
