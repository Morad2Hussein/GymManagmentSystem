

using AutoMapper;

using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.MembershipViewModels;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.UnitOfWork;



namespace GymManagementBll.Services.Classes
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _iunitOfWork;
        private readonly IMapper _mapper;
        public MembershipService(IUnitOfWork iunitOfWork, IMapper mapper)
        {
            _iunitOfWork = iunitOfWork;
            _mapper = mapper;
        }
        public bool CreateMembership(CreateMemberShipViewModel CreatedMemberShip)
        {
            try
            {
                if (!IsMemberExists(CreatedMemberShip.MemberId) || !IsPlanExists(CreatedMemberShip.PlanId)
                    || HasActiveMemberShip(CreatedMemberShip.MemberId)) return false;
                var MemberShipToCreate = _mapper.Map<MemberShip>(CreatedMemberShip);
                var Plan = _iunitOfWork.GetRepository<Plan>().GetById(CreatedMemberShip.PlanId);
                MemberShipToCreate.EndDate = DateTime.Now.AddDays(Plan!.DurationDays);
                _iunitOfWork.MembershipRepository.Add(MemberShipToCreate);
                return _iunitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteMemberShip(int MemberId)
        {
            var Repo = _iunitOfWork.MembershipRepository;
            var ActiveMemberships = Repo.GetAll(X => X.MemberId == MemberId && X.Status == "Active").FirstOrDefault();
            if (ActiveMemberships is null) return false;
            Repo.Delete(ActiveMemberships);
            return _iunitOfWork.SaveChanges() > 0;
        }
        public IEnumerable<MemberShipViewModel> GetAllMemberShips()
        {
            var MemberShips = _iunitOfWork.MembershipRepository.GetAllMembershipsWithMemberAndPlan(X => X.Status == "Active");
            if (!MemberShips.Any()) return [];
            return _mapper.Map<IEnumerable<MemberShipViewModel>>(MemberShips);
        }
        public IEnumerable<PlanSelectListViewModel> GetPlansForDropDown()
        {
            var Plans = _iunitOfWork.GetRepository<Plan>().GetAll(X => X.IsActive == true);
            return _mapper.Map<IEnumerable<PlanSelectListViewModel>>(Plans);
        }
        public IEnumerable<MemberSelectListViewModel> GetMembersForDropDown()
        {
            var Members = _iunitOfWork.GetRepository<Member>().GetAll();
            return _mapper.Map<IEnumerable<MemberSelectListViewModel>>(Members);
        }

        #region Helper Methods 

        private bool IsMemberExists(int MemberId)
        {
            return _iunitOfWork.GetRepository<Member>().Exists(X => X.Id == MemberId);
        }
        private bool IsPlanExists(int PlanId)
        {
            return _iunitOfWork.GetRepository<Plan>().Exists(X => X.Id == PlanId);
        }
        private bool HasActiveMemberShip(int memberId)
        {
            return _iunitOfWork.MembershipRepository.Exists(X => X.MemberId == memberId && X.Status == "Active");
        }


        #endregion
    }
}
