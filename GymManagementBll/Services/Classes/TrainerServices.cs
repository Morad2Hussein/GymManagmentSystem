

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
        private UnitOfWork _unitOfWork;
        public TrainerServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Get all 
        public IEnumerable<TrainerViewModels> GetAllTrinaer()
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainer is null || Trainer.Any()) return [];
            var TrainerViewModels = Trainer.Select(x => new TrainerViewModels
            {
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                
                Specialties = x.Specialties.ToString()
            });
            return  TrainerViewModels;

        }

        #endregion

        #region Create Or Add New Trainer
        public bool CreateTrainer(TrainerCreateViewModel trainerCreateViewModel)
        {
            try
            {
                // Check Email & Phone 
                if (EmailExists(trainerCreateViewModel.Email) || PhoneExists(trainerCreateViewModel.Phone)) return false;
                var trainer = new Trainer()
                {
                    Name = trainerCreateViewModel.Name,
                    Email = trainerCreateViewModel.Email,
                    Phone = trainerCreateViewModel.Phone,
                    Specialties = Enum.Parse<Specialties>(trainerCreateViewModel.Specialties),

                    Address = new Address()
                    {

                        BuildingNumber = trainerCreateViewModel.BuildingNumber,
                        City = trainerCreateViewModel.City,
                        Street = trainerCreateViewModel.Street,
                    }
                };
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
            var sessionRepo = _unitOfWork.GetRepository<Session>();

            var trainer = trainerRepo.GetById(Id);
            if (trainer is null)
                return false;

            bool hasFutureSessions = sessionRepo
                .GetAll(s => s.TrainerId == Id && s.StartDate > DateTime.Now)
                .Any();

            if (hasFutureSessions)
                return false; 

            try
            {
                trainerRepo.Delete(trainer);
                return _unitOfWork.SaveChanges() > 0; 
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Get By Id
        public TrainerViewModels? GetTrainerDetailsById(int TrainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Trainer is null) return null;
            return new TrainerViewModels()
            {
                Name = Trainer.Name,
                Specialties = Trainer.Specialties.ToString(),
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                DateOfBirth = Trainer.DateOfBirth.ToShortDateString(),
                Address = $"{Trainer.Address.BuildingNumber},{Trainer.Address.Street},{Trainer.Address.City}",


            };
        }
        #endregion


        #region tO Display Data Before Update

        public UpdateTrainerViewModel? GetTrainerUpdate(int id)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(id);
            if (Trainer is null) return null;
            return new UpdateTrainerViewModel()
            {
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                Specialties = Trainer.Specialties,
                BuildingNumber = Trainer.Address.BuildingNumber,
                City = Trainer.Address.City,
                Street = Trainer.Address.Street,



            };


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
                TrainerUpdate.Email = updateTrainerViewModel.Email;
                TrainerUpdate.Phone = updateTrainerViewModel.Phone;
                TrainerUpdate.Specialties = updateTrainerViewModel.Specialties;
                TrainerUpdate.Address.BuildingNumber = updateTrainerViewModel.BuildingNumber;
                TrainerUpdate.Address.City = updateTrainerViewModel.City;
                TrainerUpdate.Address.Street = updateTrainerViewModel.Street;
                TrainerUpdate.UpdateAt = DateTime.Now;
             return   _unitOfWork.SaveChanges() > 0;

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



        #endregion
    }
}
