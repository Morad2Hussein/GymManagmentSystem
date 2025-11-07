using GymManagementBll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPl.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMembershipService _membershipService;
        public MemberShipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
