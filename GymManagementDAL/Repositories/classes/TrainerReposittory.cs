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
    internal class TrainerReposittory : ITrainerReposittory
    {
        private readonly GymManagementDbContext _dbContext;
        public TrainerReposittory(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Trainer trainer)
        {
           _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var trainer = _dbContext.Trainers.Find(id);
            if(trainer is null ) return -1;
            _dbContext.Trainers.Remove(trainer);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll()=> _dbContext.Trainers.ToList();

        public Trainer? GetbyId(int id) => _dbContext.Trainers.Find(id);
        public int Update(Trainer trainer)
        {
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
