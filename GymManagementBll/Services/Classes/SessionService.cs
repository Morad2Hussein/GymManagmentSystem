using AutoMapper;
using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.SessioViewModesl;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.UnitOfWork;
using GymManagementSystemBLL.ViewModels.SessionViewModels;


namespace GymManagementBll.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SessionService(IUnitOfWork iunitOfWork, IMapper mapper)
        {
            _unitOfWork = iunitOfWork;
            _mapper = mapper;
        }

        #region GetAll Sessions 

        public IEnumerable<SessionViewModel> GetAllSession()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionWithTrainerAndCategory();
            if (!sessions.Any()) return [];
            var MappSessein = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in MappSessein)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlotes(session.Id);

            }
            return MappSessein;

        }
        #endregion

        #region Get Session By Id 
        public SessionViewModel? GetSessionById(int id)
        {
            var Sessions = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(id);

            if (Sessions is null) return null;
            var MappSessein = _mapper.Map<Session, SessionViewModel>(Sessions);
            MappSessein.AvailableSlots = MappSessein.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlotes(MappSessein.Id);
            return MappSessein;


        }
        #endregion


        #region Create Or Add A new Session 
        public bool CreateSession(CreateSessionViewModel createSessionViewModel)
        {
            // check category  - Trainer - DateTime - Capacity between 0 to 25

            try
            {
                if (!IsTrainder(createSessionViewModel.TrainerId)
           || !IsCategory(createSessionViewModel.CategoryId)
           || !IsDateTimeViald(createSessionViewModel.StartDate, createSessionViewModel.EndDate)
           || createSessionViewModel.Capacity > 25 || createSessionViewModel.Capacity < 0)
                    return false;
                var Session = _mapper.Map<Session>(createSessionViewModel);
                _unitOfWork.GetRepository<Session>().Add(Session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create Session" + ex.ToString());
                return false;
            }


        }

        #endregion


        #region View And Updates
        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var Sessions = _unitOfWork.SessionRepository.GetById(sessionId);
            if (Sessions == null) return null;
            if (!IsSessionAvailable(Sessions!)) return null;
            return _mapper.Map<UpdateSessionViewModel>(Sessions);

        }

        public bool UpdateSession(UpdateSessionViewModel updateSessionViewModel, int seesionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(seesionId);

                if (
                    !IsSessionAvailable(Session!) ||
                    !IsTrainder(updateSessionViewModel.TrainerId)
                    || !IsDateTimeViald(updateSessionViewModel.StartDate, updateSessionViewModel.EndDate)
            )
                    return false;
                _mapper.Map(updateSessionViewModel, Session);
                Session!.UpdateAt = DateTime.Now;

                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {

                Console.WriteLine("Update Session " + ex.ToString());
                return false;
            }

        }
        #endregion
        #region Delete Session 
        public bool RemoveSeesion(int sessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(sessionId);
                if(Session is null || !IsSessionAvailableForRemoving(Session)) return false;
                _unitOfWork.SessionRepository.Delete(Session);
                return _unitOfWork.SaveChanges() > 0;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        #endregion

        #region Service will know it 
        IEnumerable<TrainerSelectViewModel> ISessionService.GetTrainersForDropDown()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
        }

        IEnumerable<CategorySelectViewModel> ISessionService.GetCategoriesForDropDown()
        {
            var categories = _unitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(categories);
        }

      

       

        #endregion
        #region Helper
        // check of trainer - category- startdate 
        private bool IsTrainder(int trId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trId) is not null;
        }
        private bool IsCategory(int catId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(catId) is not null;
        }
        private bool IsDateTimeViald(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate;

        }
        private bool IsSessionAvailable(Session session)
        {

            if (session is null) return false;

            // If Session Completed - No Updated Allowed
            if (session.EndDate < DateTime.Now) return false;

            // If Session Started - No Updated Allowed
            if (session.StartDate <= DateTime.Now) return false;

            // If Session Has Active Bookings - No Updated Allowed
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlotes(session.Id) > 0;
            if (HasActiveBooking) return false;

            return true;

        }
        private bool IsSessionAvailableForRemoving(Session session)
        {
            if (session is null) return false;

            // If Session Started - No Delete Allowed
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;

            // Is Session Is Upcoming - No Delete Allowed
            if (session.StartDate > DateTime.Now) return false;

            // If Session Has Active Bookings - No Updated Allowed
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlotes(session.Id) > 0;
            if (HasActiveBooking) return false;

            return true;

        }




        #endregion


    }
}
