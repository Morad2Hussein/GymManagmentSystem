using AutoMapper;
using GymManagementBll.ViewModels.MemberViewModels;
using GymManagementBll.ViewModels.PlanViewModels;
using GymManagementBll.ViewModels.SessioViewModesl;
using GymManagementBll.ViewModels.TrainerModels;
using GymManagementDAL.Models.Common;
using GymManagementDAL.Models.Entities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace GymManagementBll.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            MapSession();
            MapMember();
            MapTrainer();

            MapPlan();


          

        }
       
        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                  {
                      BuildingNumber = src.BuildingNumber,
                      City = src.City,
                      Street = src.Street
                  })).ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));


            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();
            CreateMap<Member, MemberViewModel>()
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

            CreateMap<Member, MemberUpdateViewModel>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));

            CreateMap<MemberUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                    dest.UpdateAt = DateTime.Now;
                });
        }
        private void MapTrainer()
        {
            CreateMap<TrainerCreateViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));
            CreateMap<Trainer, TrainerViewModels>();
            CreateMap<Trainer, UpdateTrainerViewModel>()
                .ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dist => dist.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

            CreateMap<UpdateTrainerViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.UpdateAt = DateTime.Now;
            });
        }
        private void MapSession()
        {
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, SessionViewModel>()
                        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SessionCategory.Name))
                        .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
                        .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore()); // Will Be Calculated After Map
            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();


            CreateMap<Trainer, TrainerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>()
                .ForMember(dist => dist.Name, opt => opt.MapFrom(src => src.Name));
        }
        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();
            CreateMap<Plan, UpdatePlanViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<UpdatePlanViewModel, Plan>()
           .ForMember(dest => dest.Name, opt => opt.Ignore())
           .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
