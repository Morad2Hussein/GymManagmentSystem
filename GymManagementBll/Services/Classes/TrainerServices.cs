using AutoMapper;
using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.TrainerModels;
using GymManagementDAL.Models.Common;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.UnitOfWork;


namespace GymManagementSystemBLL.Services.Classes
{
    public class TrainerServices : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<TrainerViewModels> GetAllTrinaer()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainers is null || !Trainers.Any()) return [];

            return Trainers.Select(X => new TrainerViewModels
            {
                Id = X.Id,
                Name = X.Name,
                Email = X.Email,
                Phone = X.Phone,
                Specialties = X.Specialties.ToString()
            }); 
        }
        public bool CreateTrainer(TrainerCreateViewModel createdTrainer)
        {
            try
            {
                var Repo = _unitOfWork.GetRepository<Trainer>();

                if (IsEmailExists(createdTrainer.Email) || IsPhoneExists(createdTrainer.Phone)) return false;
                var Trainer = new Trainer()
                {
                    Name = createdTrainer.Name,
                    Email = createdTrainer.Email,
                    Phone = createdTrainer.Phone,
                    DateOfBirth = createdTrainer.DateOfBrith,
                    Specialties = createdTrainer.Specialties,
                    Gender = createdTrainer.Gender,
                    Address = new Address()
                    {
                        BuildingNumber = createdTrainer.BuildingNumber,
                        City = createdTrainer.City,
                        Street = createdTrainer.Street,
                    }
                };


                Repo.Add(Trainer);

                return _unitOfWork.SaveChanges() > 0;


            }
            catch (Exception)
            {
                return false;
            }
        }


        public TrainerViewModels? GetTrainerDetailsById(int trainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (Trainer is null) return null;


            return new TrainerViewModels
            {
                Email = Trainer.Email,
                Name = Trainer.Name,
                Phone = Trainer.Phone,
                Specialties = Trainer.Specialties.ToString()
            };
        }
        public UpdateTrainerViewModel? GetTrainerUpdate(int trainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (Trainer is null) return null;

            return new UpdateTrainerViewModel()
            {
                Name = Trainer.Name, // For Display 
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                Street = Trainer.Address.Street,
                BuildingNumber = Trainer.Address.BuildingNumber,
                City = Trainer.Address.City,
                Specialties = Trainer.Specialties
            };
        }
        public bool DeleteTrainerDetails(int trainerId)
        {
            var Repo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToRemove = Repo.GetById(trainerId);
            if (TrainerToRemove is null || HasActiveSessions(trainerId)) return false;
            Repo.Delete(TrainerToRemove);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateTrainerDetails(int Id, UpdateTrainerViewModel updatedTrainer)
        {
            var TrainerRepository = _unitOfWork.GetRepository<Trainer>();

            try
            {
                // Check Data 
                // Check if phone or email already exists
                var EmailExists = _unitOfWork.GetRepository<Trainer>()
                                   .GetAll(x => x.Email == updatedTrainer.Email && x.Id != Id);
                var PhoneExists = _unitOfWork.GetRepository<Trainer>()
                                   .GetAll(x => x.Phone == updatedTrainer.Phone && x.Id != Id);
                if (EmailExists.Any() || PhoneExists.Any()) return false;
                var TrainerUpdate = TrainerRepository.GetById(Id);
                if (TrainerUpdate is null) return false;
                _mapper.Map(updatedTrainer, TrainerUpdate);
                TrainerRepository.Update(TrainerUpdate);
                TrainerUpdate.UpdateAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0;

            }
            catch
            {
                return false;
            }
        }

        #region Helper Methods

        private bool IsEmailExists(string email)
        {
            var existing = _unitOfWork.GetRepository<Member>().GetAll(
                m => m.Email == email).Any();
            return existing;
        }

        private bool IsPhoneExists(string phone)
        {
            var existing = _unitOfWork.GetRepository<Member>().GetAll(
                m => m.Phone == phone).Any();
            return existing;
        }

        private bool HasActiveSessions(int Id)
        {
            var activeSessions = _unitOfWork.GetRepository<Session>().GetAll(
               s => s.TrainerId == Id && s.StartDate > DateTime.Now).Any();
            return activeSessions;
        }


      
        #endregion
    }
}
