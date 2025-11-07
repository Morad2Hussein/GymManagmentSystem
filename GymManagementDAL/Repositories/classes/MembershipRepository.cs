

using GymManagementDAL.Data.Context;
using GymManagementDAL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.classes
{
    public class MembershipRepository : GenericRepository<MemberShip>
    {
        private readonly GymManagementDbContext _dbContext;
        public MembershipRepository(GymManagementDbContext dbContext) : base(dbContext) {
         _dbContext = dbContext;
        }
        public IEnumerable<MemberShip> GetAllMembershipsWithMemberAndPlan(Func<MemberShip, bool> predicate)
        {
            return _dbContext.MemberShips.Include(P=>P.MemberId).Include(P=>P.PlanId).Where(predicate).ToList(); 
        }

    }
}
