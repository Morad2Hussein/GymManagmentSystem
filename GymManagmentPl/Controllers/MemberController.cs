using GymManagementBll.Services.Classes;
using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.MemberSessionViewModel;
using GymManagementBll.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPl.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        #region Get All Members
        public ActionResult Index()
        {
            var Member = _memberService.GetALLMembers();
            return View(Member);
        }
        #endregion
        #region Get Member Data 
        public ActionResult MemberDetails(int id)
        {
            var memberDetails = _memberService.GetMemberDetails(id);
            if (id <= 0)
            {
                TempData[ "ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            if (memberDetails is  null)
            {
                TempData[ "ErrorMessage"] = "Member  not found";
                return RedirectToAction(nameof(Index));
            }
            return View(memberDetails);
        }
        #endregion
        #region Get Health Record 
        public ActionResult HealthRecord(int id)
        {
            var healthRecord = _memberService.GetMemberHealthRecord(id);
            if (id <= 0){
                TempData[ "ErrorMessage"] = "Id of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }
            if (healthRecord is null)
            {
                TempData[ "ErrorMessage"] = "Member  not found";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        }
        #endregion
        #region Create Member 
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel createMemberView)

        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(key: "DataInvalid", errorMessage: "Check Data And Missing Fields");

                return View(nameof(Create), createMemberView);
            }
            bool Result = _memberService.CreateMember(createMemberView);
            if (Result)
            {
                TempData[ "SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData[ "ErrorMessage"] = "Member Failed To Create , Check Phone And Email";
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
