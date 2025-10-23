using GymManagementBll.Services.Classes;
using GymManagementBll.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPl.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService) {
            _sessionService = sessionService;
        }
        #region Get All Data
        public ActionResult Index()
        {
            var Sessions = _sessionService.GetAllSession();
            return View(Sessions);
        }

        #endregion
        #region Get Details
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Sessions Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Sessions = _sessionService.GetSessionById(id);
            if (Sessions is null)
            {
                TempData["ErrorMessage"] = "Can not Found Session";
                return RedirectToAction(nameof(Index));

            } 
            return View(Sessions);
        }
        #endregion
        #region Create
        public ActionResult Create()
        {
            LoadDropdownsForCategories();
            LoadDropdownsForTrainers();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createSessionView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(key: "DataInvalid", errorMessage: "Check Data And Missing Fields");

                return View( createSessionView);
            }
            bool Result = _sessionService.CreateSession(createSessionView);
            if (Result)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed To Create , Check Data";
            }
            return RedirectToAction("Index");

        }
        #endregion
        #region Edit
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Sessions Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var SessionEdit = _sessionService.GetSessionToUpdate(id);
            if (SessionEdit is null)
            {
         
                    TempData["ErrorMessage"] = "Can not Found Session";
                    return RedirectToAction(nameof(Index));

     
            }
            LoadDropdownsForTrainers();
            return View(SessionEdit);

        }
        [HttpPost]
        public ActionResult Edit([FromRoute]int id , UpdateSessionViewModel updateSessionView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(key: "DataMissed", "Check Missing Fields");

                return View(updateSessionView);
            }
            var SessionUpdate = _sessionService.UpdateSession( updateSessionView, id );
            if (SessionUpdate)

            {
                TempData["SuccessMessage"] = "Session Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed To Update .";
            }
            return RedirectToAction(nameof(Index));

        }

        #endregion
        #region Delete Session
        public ActionResult Delete(int id)
        {
            var session = _sessionService.GetSessionById(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(session);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _sessionService.RemoveSeesion(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Session deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Session cannot be deleted";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper Methods
        private void LoadDropdownsForCategories()
        {
            var categories = _sessionService.GetCategoriesForDropDown();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
        private void LoadDropdownsForTrainers()
        {
            var trainers = _sessionService.GetTrainersForDropDown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
       
        }
        #endregion
    }

}
