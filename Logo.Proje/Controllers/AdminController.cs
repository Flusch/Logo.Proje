using Microsoft.AspNetCore.Mvc;

namespace Logo.Proje.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() //Admin tools: insert apart, insert resident, insert bill
        {
            return View();
        }
    }
}
