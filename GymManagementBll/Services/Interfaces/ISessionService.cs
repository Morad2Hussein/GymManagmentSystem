using GymManagementBll.ViewModels.SessioViewModesl;
using GymManagementSystemBLL.ViewModels.SessionViewModels;


namespace GymManagementBll.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSession();
        // I can't understand why we used it.
        #region  
        IEnumerable<TrainerSelectViewModel> GetTrainersForDropDown();
        IEnumerable<CategorySelectViewModel> GetCategoriesForDropDown(); 
        #endregion
        SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionViewModel createSessionViewModel);
        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
        bool UpdateSession(UpdateSessionViewModel updateSession, int id );
        bool RemoveSeesion(int sessionId);
   
    }
}

