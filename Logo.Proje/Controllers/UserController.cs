using Logo.Proje.Business.Abstracts;
using Logo.Proje.Domain.Entities;
using Logo.Proje.Domain.MongoDbEntities;
using Logo.Proje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace Logo.Proje.Controllers
{
    [Authorize(Roles = "Resident")]
    public class UserController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IBillService _billService;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public UserController(IMessageService messageService,
            IBillService billService,
            IPaymentService paymentService,
            UserManager<CustomIdentityUser> userManager)
        {
            _messageService = messageService;
            _billService = billService;
            _paymentService = paymentService;
            _userManager = userManager;
        }
        [Authorize(Roles = "A")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MyBills()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            return View(_billService.GetMyBills(user.Id));
        }
        // GET: PayBill/5
        public IActionResult PayBill(int? id)
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
        // POST: PayBill/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayBill(int? id, [Bind("Type,ApartmentId,Amount,BillDate,DueDate,IsPaid,PaymentDate,Id")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                var user = await _userManager.GetUserAsync(User);
                _paymentService.AddPayment(new Payment
                {
                    CardId = -1,
                    BillId = bill.Id,
                    CreatedBy = user.Id,
                    PaymentDate = DateTime.Now,
                });
                _billService.UpdateBill(new Bill
                {
                    Id = bill.Id,
                    ApartmentId = bill.ApartmentId,
                    IsPaid = true,
                    PaymentDate = DateTime.Now,
                    LastUpdatedBy = user.Id
                });
                return RedirectToAction(nameof(MyBills));
            }
            /*
            
            ViewData["MyBills"] = new SelectList(_billService.GetMyBills(user.Id), "Id", "Amount"); //fix: find a way to show the 'residentName(email)' instead of id
            */
            return View(bill);
        }
        public async Task<IActionResult> MyMessages()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            return View(_messageService.GetMyMessages(user.Id));
        }
        public void GetCurrentUser(){ } //fix: implement this with using HttpContextAccessor
    }
}
