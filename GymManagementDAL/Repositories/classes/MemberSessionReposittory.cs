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
    internal class MemberSessionReposittory : IMemberSessionReposittory
    {
        private readonly GymManagementDbContext _dbContext;
        public MemberSessionReposittory (GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(MemberSession memberSession)
        {
           _dbContext.Add(memberSession);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var membersession = _dbContext.MemberSessions.Find(id);
            if (membersession is  null) return -1;
            _dbContext.MemberSessions.Remove(membersession);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<MemberSession> GetAll()=> _dbContext.MemberSessions.ToList();

        public MemberSession? GetById(int id) => _dbContext.MemberSessions.Find(id);

        public int Update(MemberSession memberSession)
        {
           _dbContext.MemberSessions.Update(memberSession);
            return _dbContext.SaveChanges();
        }
    }
}
