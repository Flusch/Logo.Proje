using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logo.Proje.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index() //Admin tools: insert apart, insert resident, insert bill
        {
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }
    }
}
