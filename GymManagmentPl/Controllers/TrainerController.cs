using GymManagementBll.Services.Classes;
using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.TrainerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer  not found";
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
        [HttpPost]
        public ActionResult Create(TrainerCreateViewModel trainerCreateView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(key: "DataInvalid", errorMessage: "Check Data And Missing Fields");
                return View(nameof(Create), trainerCreateView);
            }
            bool Result = _trainerService.CreateTrainer(trainerCreateView);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Create , Check Your Data";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion
        #region Edit 
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = _trainerService.GetTrainerUpdate(id);
            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer  not found";
                return RedirectToAction(nameof(Index));
            }

            return View(Trainer);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateTrainerViewModel updateTrainerView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(key: "DataMissed", "Check Missing Fields");

                return View(updateTrainerView);
            }
            var trainerUpdate = _trainerService.UpdateTrainerDetails(id, updateTrainerView);
            if (trainerUpdate)

            {
                TempData["SuccessMessage"] = "Trainer Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Update .";
            }
            return RedirectToAction(nameof(Index));



        }
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErorrMessage"] = "Id of Trainer Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = _trainerService.GetTrainerDetailsById(id);
            if (Trainer is null)
            {
                TempData["ErorrMessage"] = "Trainer not found ";
                return RedirectToAction(nameof(Index));

            }
            ViewBag.TrainerId = id;
            ViewBag.TrainerName = Trainer.Name;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm] int id)
        {
            var member = _trainerService.DeleteTrainerDetails(id);
            if (member)

            {
                TempData["SuccessMessage"] = "Trainer Delete Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Delete .";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }

}
