using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Logo.Proje.DataAccess.EntityFramework;
using Logo.Proje.Data;
using Microsoft.AspNetCore.Identity;
using Logo.Proje.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Logo.Proje.Controllers
{
    //fix: create a resident service and move the logic to it
    [Authorize(Roles = "Admin, Manager")]
    public class ResidentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ApplicationDbContext _userContext;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public ResidentController(AppDbContext context, ApplicationDbContext userContext, UserManager<CustomIdentityUser> userManager)
        {
            _context = context;
            _userContext = userContext;
            _userManager = userManager;
        }

        // GET: Resident
        public IActionResult Index()
        {
            return View(_userManager.GetUsersInRoleAsync(Enums.Roles.Resident.ToString()).Result.ToList());
        }

        // GET: Apartment/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Resident/Create
        // Using register page for creating new residents
        public IActionResult Create()
        {
            return View();
        }

        // GET: Resident/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _userContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Apartment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Name,Surname,IdentityNo,HasCar,CarPlate,Username,Email,Phone,Id")] CustomIdentityUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //fix

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Apartment/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _userContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Apartment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            return RedirectToAction(nameof(Index));
        }

        private bool ResidentExists(string id)
        {
            return _userContext.Users.Any(e => e.Id == id);
        }
    }
}
