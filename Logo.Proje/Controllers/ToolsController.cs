using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logo.Proje.Controllers
{
    public class ToolsController : Controller
    {
        [Authorize(Roles = "A")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminTools()
        {
            return View();
        }
        [Authorize(Roles = "Manager")]
        public IActionResult ManagerTools()
        {
            return View();
        }
    }
}
