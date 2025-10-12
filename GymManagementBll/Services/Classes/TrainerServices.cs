

using AutoMapper;
using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.TrainerModels;
using GymManagementDAL.Models.Common;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.Models.Enum;
using GymManagementDAL.UnitOfWork;

namespace GymManagementBll.Services.Classes
{
    public class TrainerServices : ITranierService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TrainerServices(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get all 
        public IEnumerable<TrainerViewModels> GetAllTrinaer()
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainer is null || !Trainer.Any()) return [];
            var TrainerViewModels = _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerViewModels>>(Trainer);
            return TrainerViewModels;

        }

        #endregion

        #region Create Or Add New Trainer
        public bool CreateTrainer(TrainerCreateViewModel trainerCreateViewModel)
        {
            try
            {
                // Check Email & Phone 
                if (EmailExists(trainerCreateViewModel.Email) || PhoneExists(trainerCreateViewModel.Phone)) return false;
                var trainer = _mapper.Map<Trainer>(trainerCreateViewModel);
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        public bool DeleteTrainerDetails(int Id)
        {
            var trainerRepo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToDelete = trainerRepo.GetById(Id);
            if (TrainerToDelete is null || HasActiveSession(Id)) return false;
            trainerRepo.Delete(TrainerToDelete);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region Get By Id
        public TrainerViewModels? GetTrainerDetailsById(int TrainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Trainer is null) return null;
            var TrainerView = _mapper.Map<Trainer, TrainerViewModels>(Trainer);
            return TrainerView;
        }
        #endregion


        #region tO Display Data Before Update

        public UpdateTrainerViewModel? GetTrainerUpdate(int id)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(id);
            if (Trainer is null) return null;
            var UpdateTrainerViewModel = _mapper.Map<UpdateTrainerViewModel>(Trainer);


            return new UpdateTrainerViewModel();


        }
        #endregion

        #region Update Date
        public bool UpdateTrainerDetails(int Id, UpdateTrainerViewModel updateTrainerViewModel)
        {
            var TrainerRepository = _unitOfWork.GetRepository<Trainer>();

            try
            {
                // Check Data 
                var TrainerUpdate = TrainerRepository.GetById(Id);
                if (TrainerUpdate is null) return false;
                _mapper.Map(updateTrainerViewModel, TrainerUpdate);
                TrainerRepository.Update(TrainerUpdate);
                TrainerUpdate.UpdateAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region Helper Function Check Email&Phone
        private bool EmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(me => me.Email == email).Any();
        }
        private bool PhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(me => me.Phone == phone).Any();
        }
        private bool HasActiveSession(int id)
        {
            var activeSession = _unitOfWork.GetRepository<Session>().GetAll(
                 ac => ac.TrainerId == id && ac.StartDate == DateTime.Now).Any();
            return activeSession;
        }



        #endregion
    }
}
