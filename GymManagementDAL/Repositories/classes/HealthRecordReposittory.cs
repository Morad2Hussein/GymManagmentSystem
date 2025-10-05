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
    internal class HealthRecordReposittory : IHealthRecordReposittory
    {
        private readonly GymManagementDbContext _dbContext;
        public HealthRecordReposittory(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(HealthRecord healthRecord)
        {
            _dbContext.HealthRecords.Add(healthRecord);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var health = _dbContext.HealthRecords.Find(id);
            if (health is null) return -1;
            _dbContext.HealthRecords.Remove(health);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<HealthRecord> GetAll()=>    _dbContext.HealthRecords.ToList();


        public HealthRecord? GetById(int id) => _dbContext.HealthRecords.Find(id);

        public int Update(HealthRecord healthRecord)
        {
           _dbContext.HealthRecords.Update(healthRecord);
            return _dbContext.SaveChanges();
        }
    }
}
