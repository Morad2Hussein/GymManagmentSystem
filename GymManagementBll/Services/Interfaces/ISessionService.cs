using GymManagementSystemBLL.ViewModels.SessionViewModels;


namespace GymManagementBll.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSession();
       SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionViewModel createSessionViewModel);

    }
}
