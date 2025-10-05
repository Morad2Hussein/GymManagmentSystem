using GymManagementDAL.Data.Context;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.classes
{
    internal class SessionReposittory : ISessionRepository
    {
        private readonly GymManagementDbContext _dbContext;
        public SessionReposittory(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Session session)
        {
           _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var session = _dbContext.Sessions.Find(id);
            if(session is null ) return -1;
            _dbContext.Sessions.Remove(session);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAll()=> _dbContext.Sessions.ToList();

        public Session? GetbyId(int id) => _dbContext.Sessions.Find(id);

        public int Update(Session session)
        {
           _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
