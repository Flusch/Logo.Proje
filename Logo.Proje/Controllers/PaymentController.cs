using Logo.Proje.Business.Abstracts;
using Logo.Proje.Data;
using Logo.Proje.Domain.MongoDbEntities;
using Logo.Proje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Logo.Proje.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _userContext;
        private readonly IPaymentService _paymentService;
        private readonly IMongoCollection<Payment> _payments;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public PaymentController(IPaymentService paymentService,
            ApplicationDbContext usercontext,
            IMongoClient mongoClient,
            UserManager<CustomIdentityUser> userManager)
        {
            _userContext = usercontext;
            _paymentService = paymentService;
            _payments = mongoClient.GetDatabase("LogoDb").GetCollection<Payment>("payments");
            _userManager = userManager;

        }

        // GET: Card
        public IActionResult Index()
        {
            return View(_paymentService.GetAllPayments());
        }

        // GET: Card/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _paymentService.GetPaymentById(x => x.Id == id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // GET: Card/Create
        public async Task<IActionResult> Create()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var id = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;

            ViewData["OwnerId"] = new SelectList(_userContext.Users, "Id", "Email"); //fix: find a way to show the 'fullname(email)' instead of just email
            return View();
        }

        // POST: Card/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CardId,BillId,PaymentDate,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _paymentService.AddPayment(payment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_userContext.Users, "Id", "Email"); //fix: find a way to show the 'fullname(email)' instead of just email
            return View(payment);
        }

        // GET: Card/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var card = _paymentService.GetPaymentById(x => x.Id == id);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_userContext.Users, "Id", "Email"); //fix: find a way to show the 'fullname(email)' instead of just email
            return View(card);
        }

        // POST: Card/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("CardId,BillId,PaymentDate,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _paymentService.UpdatePayment(payment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(payment.Id))
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
            ViewData["OwnerId"] = new SelectList(_userContext.Users, "Id", "Email"); //fix: find a way to show the 'fullname(email)' instead of just email
            return View(payment);
        }

        // GET: Card/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var card = _paymentService.GetPaymentById(x => x.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Card/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _paymentService.DeletePayment(new Payment { Id = id });
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(string id)
        {
            return _payments.Find<Payment>(e => e.Id == id).Any();
        }
    }
}
