using AutoMapper;
using GymManagementBll.Services.AttachmentService;
using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.MemberViewModels;
using GymManagementDAL.Models.Common;
using GymManagementDAL.Models.Entities;

using GymManagementDAL.UnitOfWork;


namespace GymManagementBll.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper , IAttachmentService attachmentService)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }


        #region Get All Date From Member
        public IEnumerable<MemberViewModel> GetALLMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll();
            if (Members == null || !Members.Any()) return [];
            var MemberMapper = _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(Members);
            return MemberMapper;
        }
        #endregion
        #region Create Or Add A New Member 
        public bool CreateMember(CreateMemberViewModel createMember)
        {

            try
            {
                // Email and Phone Is Unique 

                // 1- Check If I have a Email or phone or not 
                if (PhoneExists(createMember.Phone) || EmailExists(createMember.Email)) return false;
                // using Attachment to upload photo
                var PhotoName = _attachmentService.Upload("members", createMember.PhotoFile);
                if(string.IsNullOrEmpty(PhotoName)) return false;
                // if i have email or phone  can not create else can create 
                var member = _mapper.Map<Member>(createMember);
                member.Photo = PhotoName;
                _unitOfWork.GetRepository<Member>().Add(member);
                    var IsCreated =  _unitOfWork.SaveChanges() > 0;
                if (!IsCreated)
                {
                    _attachmentService.Delete(PhotoName, "members");
                    return false;
                }
                else
                {
                    return IsCreated;
                }

            }

            catch 
            {

                return false;

            }
        }


        #endregion

        #region GetAll Details
        MemberViewModel? IMemberService.GetMemberDetails(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null) return null;
            var ViewModel = _mapper.Map<MemberViewModel>(Member);
            var ActiveMemberShip = _unitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == MemberId || ms.Status == "Active")
                                                   .FirstOrDefault();
            if (ActiveMemberShip is not null)
            {
                ViewModel.MemberShipStartDate = ActiveMemberShip.CreateAt.ToShortDateString();
                ViewModel.MemberShipEndDate = ActiveMemberShip.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                ViewModel.PlanName = plan?.Name;
            }
            return ViewModel;

        }

        #endregion
        #region GetAll Health Record Details
        public HealthRecordViewModel? GetMemberHealthRecord(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);

            if (MemberHealthRecord is null) return null;

            var Healt = _mapper.Map<HealthRecordViewModel>(MemberHealthRecord);
            return Healt;


        }

        #endregion
        #region Update
        // Get Data To UPdate
        public MemberUpdateViewModel? GetMemberUpdateDetails(int MemberId)
        {

            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null) return null;
            var MemberUpdate = _mapper.Map<MemberUpdateViewModel>(Member);
            return MemberUpdate;

        }
        // Update Date

        public bool UpdateMemberDetails(int id, MemberUpdateViewModel memberUpdateDetails)
        {
            var memberRepository = _unitOfWork.GetRepository<Member>();
            try
            {
                // Check if phone or email already exists
                var EmailExists = _unitOfWork.GetRepository<Member>()
                                   .GetAll(x => x.Email == memberUpdateDetails.Email && x.Id != id);
                var PhoneExists = _unitOfWork.GetRepository<Member>()
                                   .GetAll(x => x.Phone == memberUpdateDetails.Phone && x.Id != id);
                if(EmailExists.Any() || PhoneExists.Any()) return false;

                var memberToUpdate = memberRepository.GetById(id);
                if (memberToUpdate is null)
                    return false;

                // Use AutoMapper to map the update view model to the existing entity
                _mapper.Map(memberUpdateDetails, memberToUpdate);

                memberRepository.Update(memberToUpdate);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Remove Member 
        //public bool RenewMember(int MemberId)
        //{
        //    var MemberRepositiry = _unitOfWork.GetRepository<Member>();
        //    var MemberShipRepo = _unitOfWork.GetRepository<MemberShip>();
        //    var Member = MemberRepositiry.GetById(MemberId);
        //    if (Member is null) return false;
        //    var HasActiveMemberSession = _unitOfWork.GetRepository<MemberSession>().GetAll(ms => ms.MemberId == MemberId && ms.Session.StartDate > DateTime.Now).Any();
        //    if (HasActiveMemberSession) return false;
        //    var MemberShips = MemberShipRepo.GetAll(ms => ms.MemberId == MemberId);
        //    try
        //    {
        //        if (MemberShips.Any())
        //        {
        //            foreach (var memberShip in MemberShips)
        //            {
        //                MemberShipRepo.Delete(memberShip);
        //            }
        //        }
        //        MemberRepositiry.Delete(Member);
        //        return _unitOfWork.SaveChanges() > 0;

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //} 
        public bool RemoveMember(int MemberId)
        {
            var Repo = _unitOfWork.GetRepository<Member>();
            var Member = Repo.GetById(MemberId);
            if (Member is null) return false;
            var sessionIds = _unitOfWork.GetRepository<MemberSession>().GetAll(
               b => b.MemberId == MemberId).Select(S => S.SessionId); // 1 5 8

            var hasFutureSessions = _unitOfWork.GetRepository<Session>()
                .GetAll(S => sessionIds.Contains(S.Id) && S.StartDate > DateTime.Now).Any();

            if (hasFutureSessions) return false;

            var MemberShips = _unitOfWork.GetRepository<MemberShip>().GetAll(X => X.MemberId == MemberId);

            try
            {
                if (MemberShips.Any())
                {
                    foreach (var membership in MemberShips)
                        _unitOfWork.GetRepository<MemberShip>().Delete(membership);
                }
                _unitOfWork.GetRepository<Member>().Delete(Member);
                var IsDeleted= _unitOfWork.SaveChanges() > 0;
                if (IsDeleted)
                
                    _attachmentService.Delete(Member.Photo, " members");
                    return IsDeleted;
                
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region Helper Function Check Email&Phone
        private bool EmailExists(string email)
        {
            var EmailExist = _unitOfWork.GetRepository<Member>().GetAll(me => me.Email == email);
            return EmailExist.Any();
        }
        private bool PhoneExists(string phone)
        {
            var PhoneExist = _unitOfWork.GetRepository<Member>().GetAll(me => me.Phone == phone);
            return PhoneExist.Any();
        }


        #endregion
    }
}

