using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Logo.Proje.DataAccess.EntityFramework;
using Logo.Proje.Domain.Entities;
using Logo.Proje.Business.Abstracts;
using Microsoft.AspNetCore.Authorization;

namespace Logo.Proje.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class BillController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBillService _billService;

        public BillController(AppDbContext context, IBillService billService)
        {
            _context = context;
            _billService = billService;
        }

        // GET: Bill
        public IActionResult Index()
        {
            return View(_billService.GetAllBills());
        }

        // GET: Bill/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var bill = _billService.GetBillById(x => x.Id == id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        // GET: Bill/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id"); //fix: find a way to show the 'residentName(email)' instead of id
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Type,ApartmentId,Amount,BillDate,DueDate,IsPaid,PaymentDate,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _billService.AddBill(bill);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id"); //fix: find a way to show the 'residentName(email)' instead of id
            return View(bill);
        }

        // GET: Bill/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bill = _billService.GetBillById(x => x.Id == id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id", bill.ApartmentId); //fix: find a way to show the 'residentName(email)' instead of id
            return View(bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Type,ApartmentId,Amount,BillDate,DueDate,IsPaid,PaymentDate,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _billService.UpdateBill(bill);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartments, "Id", "Id", bill.ApartmentId); //fix: find a way to show the 'residentName(email)' instead of id
            return View(bill);
        }

        // GET: Bill/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bill = _billService.GetBillById(x => x.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _billService.DeleteBill(new Bill { Id = id, LastUpdatedBy = "SYSTEM" });
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
