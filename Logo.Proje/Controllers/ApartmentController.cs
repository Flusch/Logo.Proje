using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Logo.Proje.DataAccess.EntityFramework;
using Logo.Proje.Domain.Entities;
using Logo.Proje.Business.Abstracts;

namespace Logo.Proje.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IApartmentService _apartmentService;

        public ApartmentController(AppDbContext context, IApartmentService apartmentService)
        {
            _context = context;
            _apartmentService = apartmentService;
        }

        // GET: Apartment
        public IActionResult Index()
        {
            return View(_apartmentService.GetAllApartments());
        }

        // GET: Apartment/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var apartment = _apartmentService.GetApartmentById(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Block,Floor,Number,RoomCount,IsSomeoneLiving,ResidentId,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _apartmentService.AddApartment(apartment);
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Apartment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = _apartmentService.GetApartmentById(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            return View(apartment);
        }

        // POST: Apartment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Block,Floor,Number,RoomCount,IsSomeoneLiving,ResidentId,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _apartmentService.UpdateApartment(apartment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Apartment/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var apartment = _apartmentService.GetApartmentById(x => x.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _apartmentService.DeleteApartment(new Apartment { Id = id, LastUpdatedBy = "Yavuz Selim" });
            /*var apartment = await _context.Apartments.FindAsync(id);
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.Id == id);
        }
    }
}
