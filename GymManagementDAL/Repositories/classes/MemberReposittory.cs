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
    internal class MemberReposittory : IMemberReposittory
    {
        private readonly GymManagementDbContext _dbContext;
        public MemberReposittory(GymManagementDbContext dbContext) {
            _dbContext = dbContext;
        }
        public int AddMember(Member member)
        {
            _dbContext.Members.Add(member);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var member = _dbContext.Members.Find(id);
            if (member is null) return -1;
            _dbContext.Members.Remove(member);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Member> GetAll() => _dbContext.Members.ToList();


        public Member? GetbyId(int id) => _dbContext.Members.Find(id);

        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
