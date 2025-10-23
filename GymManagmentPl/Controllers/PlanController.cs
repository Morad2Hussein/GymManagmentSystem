using GymManagementBll.Services.Classes;
using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.PlanViewModels;
using GymManagementDAL.Models.Entities;
using GymManagementSystemBLL.Services.Classes;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPl.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanServiec _planServiec;
        public PlanController(IPlanServiec planServiec)
        {
            _planServiec = planServiec;

        }
        #region GetAllDate 
        public ActionResult Index()
        {
            var Plan = _planServiec.GetAllPlans();
            return View(Plan);
        }

        #endregion
        #region Get Data  Details
         public ActionResult Details(int id)
        {
            
            
                if (id <= 0)
                {
                    TempData["ErrorMessage"] = "Id of Plan Can Not Be 0 Or Negative Number";
                    return RedirectToAction(nameof(Index));
                }
                var Plan = _planServiec.GetPlanById(id);
                if (Plan is null)
                {
                    TempData["ErrorMessage"] = "Plan  not found";
                    return RedirectToAction(nameof(Index));
                }
                return View(Plan);
            
        }
        #endregion
        #region Edit 
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Plan Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            var Plan = _planServiec.UpdatePlanById(id);
            if (Plan is null)
            {
                TempData["ErrorMessage"] = "Plan  not found";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute]  int id , UpdatePlanViewModel updatePlanView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(key: "DataMissed", "Check Missing Fields");

                return View(updatePlanView);
            }
            var planUpdate = _planServiec.UpdatePlan(id, updatePlanView);   
            if (planUpdate)

            {
                TempData["SuccessMessage"] = "Plan Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed To Update .";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion
        #region Activate
        [HttpPost]
        public ActionResult Activate([FromRoute] int id )
        {
            if(id <= 0)

            {
                TempData["ErrorMessage"] = "Id Of Plan Can Not Be 0 Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var result = _planServiec.ToggleStatus(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Plan deactivated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to deactivate plan.";
            }
            return RedirectToAction(nameof(Index));


        }

        #endregion
    }
}
