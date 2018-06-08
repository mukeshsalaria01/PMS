using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            ViewData["TraceIdentifier"] = HttpContext.TraceIdentifier;
            return View();
        }
    }
}