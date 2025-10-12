using AutoMapper;
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
            CreateMap<Session, SessionViewModel>()
                    .ForMember(de => de.CategoryName, Option => Option.MapFrom(mp => mp.SessionCategory.Name))
                     .ForMember(de => de.TrainerName, option => option.MapFrom(mp => mp.SessionTrainer.Name))
                     .ForMember(de => de.AvailableSlots, option => option.Ignore());
            // here i make AvailableSlots Ignore i want to make it soon 
            CreateMap<SessionViewModel, Session>();


        }
    }
}
