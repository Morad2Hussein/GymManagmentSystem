using GymManagementBll.Services.Classes;
using GymManagementBll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPl.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        #region getall data
        public ActionResult Index()
        {
            var Trainer = _trainerService.GetAllTrinaer();
            return View(Trainer);
        }
        #endregion
        #region Get Data By Id
        public ActionResult TrainerDetails(int id)
        {
            var trainer = _trainerService.GetTrainerDetailsById(id);
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Member  not found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        #endregion
        #region Create 
        public ActionResult Create()
        {
            return View();
        }
        #endregion
    }
}
