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
  
        public MemberService( IUnitOfWork unitOfWork ) {
        
         _unitOfWork = unitOfWork;
        }
         #region old Context
        //public MemberService(
        //IGenericRepository<Member> memberRepository
        //   , IGenericRepository<MemberShip> memberShipRepository,
        //    IGenericRepository<HealthRecord> healthRecordRepository,
        //   IPlanReposittory planReposittory,
        //   IGenericRepository<MemberSession> memberSession


        // )
        //{
        //_memberRepository = memberRepository;
        //_memberShipRepository = memberShipRepository;
        //_planReposittory = planReposittory;
        //_healthRecordRepository = healthRecordRepository;
        //_memberSession = memberSession;
        //} 
        #endregion

        #region Get All Date From Member
        public IEnumerable<MemberViewModel> GetALLMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll();
            if (Members == null || !Members.Any()) return [];
            var MemberViewodel = Members.Select(M => new MemberViewModel
            {
                Id = M.Id,
                Name = M.Name,
                Email = M.Email,
                Phone = M.Phone,
                Photo = M.Photo,
                Gender = M.Gender.ToString(),

            });
            return MemberViewodel;
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
                // if i have email or phone  can not create else can create 
                var member = new Member()
                {
                    Name = createMember.Name,
                    Email = createMember.Email,
                    Phone = createMember.Phone,
                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBrith,
                    Address = new Address()
                    {
                        BuildingNumber = createMember.BuildingNumber,
                        Street = createMember.Street,
                        City = createMember.City,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Heigth = createMember.HealthRecordViewModel.Heigth,
                        Weigth = createMember.HealthRecordViewModel.Weight,
                        BloodType = createMember.HealthRecordViewModel.BloodType,
                        Note = createMember.HealthRecordViewModel.Note,

                    }

                };
                       _unitOfWork.GetRepository<Member>().Add(member) ;
                   return _unitOfWork.SaveChanges()>0;
            }
            catch (Exception)
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
            var ViewModel = new MemberViewModel()
            {
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.Phone,
                Gender = Member.Gender.ToString(),
                DateOfBirth = Member.DateOfBirth.ToShortDateString(),
                Address = $"{Member.Address.BuildingNumber} - {Member.Address.Street} - {Member.Address.City}",
                Photo = Member.Photo,




            };
            var ActiveMemberShip = _unitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == MemberId || ms.Status == "Active")
                                                   .FirstOrDefault();
            if (ActiveMemberShip is not null)
            {
                ViewModel.MemberShipStartDate = ActiveMemberShip.CreateAt.ToShortDateString();
                ViewModel.MemberShipEndDate = ActiveMemberShip.EndDate.ToShortDateString();
                var Plan = _unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                ViewModel.PlanName = Plan?.Name;
            }
            return ViewModel;

        }

        #endregion
        #region GetAll Health Record Details
        public HealthRecordViewModel? GetMemberHealthRecord(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);

            if (MemberHealthRecord is null) return null;
            return new HealthRecordViewModel()
            {

                Heigth = MemberHealthRecord.Heigth,
                Weight = MemberHealthRecord.Weigth,
                BloodType = MemberHealthRecord.BloodType,
                Note = MemberHealthRecord.Note,

            };


        }

        #endregion
        #region Update
        // Get Data To UPdate
        public MemberUpdateViewModel? GetMemberUpdateDetails(int MemberId)
        {

            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null) return null;
            return new MemberUpdateViewModel()
            {
                Email = Member.Email,
                Phone = Member.Phone,
                BuildingNumber = Member.Address.BuildingNumber,
                Street = Member.Address.Street,
                City = Member.Address.City,



            };

        }
        // Update Date
        public bool UpdateMemberDetails(int id, MemberUpdateViewModel memberUpdateDetails)
        {
            var MemberRepositiry = _unitOfWork.GetRepository<Member>();
            try
            {
                // Email and Phone Is Unique 

                // 1- Check If I have a Email or phone or not 
                if (PhoneExists(memberUpdateDetails.Phone) || EmailExists(memberUpdateDetails.Email)) return false;
                // if i have email or phone  can not create else can create 
                var MemberUpdate = MemberRepositiry.GetById(id);
                if (MemberUpdate is null) return false;
                MemberUpdate.Email = memberUpdateDetails.Email;
                MemberUpdate.Phone = memberUpdateDetails.Phone;
                MemberUpdate.Address.BuildingNumber = memberUpdateDetails.BuildingNumber;
                MemberUpdate.Address.Street = memberUpdateDetails.Street;
                MemberUpdate.Address.City = memberUpdateDetails.City;
                MemberUpdate.UpdateAt = DateTime.Now;
                MemberRepositiry.Update(MemberUpdate); // return  
                 MemberRepositiry.Update(MemberUpdate);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }


        }
        #endregion

        public bool RenewMember(int MemberId)
        {
            var MemberRepositiry = _unitOfWork.GetRepository<Member>();
            var MemberShipRepo = _unitOfWork.GetRepository<MemberShip>();
            var Member = MemberRepositiry.GetById(MemberId);
            if (Member is null) return false;
            var HasActiveMemberSession = _unitOfWork.GetRepository<MemberSession>().GetAll(ms => ms.MemberId == MemberId && ms.Session.StartDate > DateTime.Now).Any();
            if(HasActiveMemberSession) return false;
            var MemberShips = MemberShipRepo.GetAll(ms=>ms.MemberId == MemberId);
            try
            {
                if (MemberShips.Any())
                {
                    foreach (var memberShip in MemberShips)
                    {
                        MemberShipRepo.Delete(memberShip);
                    }
                }
                   MemberRepositiry.Delete(Member) ;
                return _unitOfWork.SaveChanges() > 0;
                 
            }
            catch (Exception)
            {
                return false;
            }

        }

        #region Helper Function Check Email&Phone
        private bool EmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(me => me.Email == email).Any();
        }
        private bool PhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(me => me.Phone == phone).Any();
        }

       
        #endregion
    }
}

