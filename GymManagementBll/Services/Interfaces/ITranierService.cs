
using GymManagementBll.ViewModels.TrainerModels;

namespace GymManagementBll.Services.Interfaces
{
    public interface ITranierService
    {
        IEnumerable<TrainerViewModels> GetAllTrinaer();
        TrainerViewModels? GetTrainerDetailsById(int TrainerId);
        bool CreateTrainer(TrainerCreateViewModel trainerCreateViewModel);
        UpdateTrainerViewModel? GetTrainerUpdate(int id);
        bool UpdateTrainerDetails(   int Id, UpdateTrainerViewModel updateTrainerViewModel);

        bool DeleteTrainerDetails(int Id);


    }
}
