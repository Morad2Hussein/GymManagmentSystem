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
    internal class MemberShipReposittory : IMemberShipReposittory
    {
        private readonly GymManagementDbContext _dbContext;
        public MemberShipReposittory(GymManagementDbContext dbContext) {
        _dbContext = dbContext;
        }

        public int Add(MemberShip membership)
        {
            _dbContext.MemberShips.Add(membership);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var memberShip = _dbContext.MemberShips.Find(id);
            if(memberShip is null ) return -1;
            _dbContext.MemberShips.Remove(memberShip);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<MemberShip> GetAll()=> _dbContext.MemberShips.ToList();

        public MemberShip? GetbyId(int id) => _dbContext.MemberShips.Find(id);

        public int Update(MemberShip membership)
        {
            _dbContext.MemberShips.Update(membership);
            return _dbContext.SaveChanges();
        }
    }
}
