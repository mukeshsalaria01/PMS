using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMS.Web.UI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}