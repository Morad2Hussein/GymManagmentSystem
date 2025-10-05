using GymManagementDAL.Data.Context;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.classes
{
    internal class PlanReposittory : IPlanReposittory
    {
        private readonly GymManagementDbContext _dbContext;
        public PlanReposittory(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var plan = _dbContext.Plans.Find(id);
            if (plan is null) return -1;
            _dbContext.Plans.Remove(plan);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAll()=> _dbContext.Plans.ToList();
        public Plan? GetbyId(int id)=> _dbContext.Plans.Find(id);

        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
