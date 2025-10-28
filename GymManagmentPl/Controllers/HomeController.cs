using GymManagementBll.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPl.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
         private readonly IAnalyticsService _analyticsService;
        public HomeController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        public IActionResult Index()
        {
            var analyticsData = _analyticsService.GetAnalyticsData();
            return View(analyticsData);
        }
    }
}
