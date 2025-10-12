using AutoMapper;
using GymManagementBll.ViewModels.MemberViewModels;
using GymManagementDAL.Models.Common;
using GymManagementDAL.Models.Entities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace GymManagementBll.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

            // دي الحاجات اللي مش  هفدر اعملها علي طول في لازم ابدا اعرفها الاول و اعرف البيانات هتجي ازاي و منين  
            // mapping
            #region Sessiom aUTO Mapper
            CreateMap<Session, SessionViewModel>()
                      .ForMember(de => de.CategoryName, Option => Option.MapFrom(mp => mp.SessionCategory.Name))
                       .ForMember(de => de.TrainerName, option => option.MapFrom(mp => mp.SessionTrainer.Name))
                       .ForMember(de => de.AvailableSlots, option => option.Ignore());
            // here i make AvailableSlots Ignore i want to make it soon 
            CreateMap<SessionViewModel, Session>();

            #endregion
            // Member Aouto Mapping

            #region Mapping Member 
            CreateMap<MemberViewModel, Member>();
            // Map from CreateMemberViewModel to Member
            #region Create Mapper
            CreateMap<CreateMemberViewModel, Member>()
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBrith))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                    .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));

            // Map Address
            CreateMap<CreateMemberViewModel, Address>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));
            #endregion

            // Map HealthRecord
            #region HealthRecord Mapper
            CreateMap<HealthRecordViewModel, HealthRecord>()
                 .ForMember(dest => dest.Heigth, opt => opt.MapFrom(src => src.Heigth))
                 .ForMember(dest => dest.Weigth, opt => opt.MapFrom(src => src.Weight))
                 .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.BloodType))
                 .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note));
            #endregion


            // to display 
            #region Display 
            CreateMap<Member, MemberViewModel>()
                                   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                                   .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("MMM dd, yyyy")))
                                   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} -          {src.Address.City}"))
                                   .ForMember(dest => dest.PlanName, opt => opt.Ignore())
                                   .ForMember(dest => dest.MemberShipStartDate, opt => opt.Ignore())
                                   .ForMember(dest => dest.MemberShipEndDate, opt => opt.Ignore());
            #endregion
            // Update 
            #region UpdateMapper

            CreateMap<Member, MemberUpdateViewModel>()
                    .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                    .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            

            CreateMap<MemberUpdateViewModel, Address>();

            #endregion
            #endregion

        }
    }
}
