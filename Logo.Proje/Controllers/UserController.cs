using Logo.Proje.Business.Abstracts;
using Logo.Proje.Domain.MongoDbEntities;
using Logo.Proje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> MyBillsAsync()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            return View(_billService.GetMyBills(user.Id));
        }
        public async Task<IActionResult> PayBillAsync()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            ViewData["MyBills"] = new SelectList(_billService.GetMyBills(user.Id), "Id", "Amount"); //fix: find a way to show the 'residentName(email)' instead of id
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayBillAsync([Bind("Type,ApartmentId,Amount,BillDate,DueDate,IsPaid,PaymentDate,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _paymentService.AddPayment(payment);
                return RedirectToAction(nameof(Index));
            }
            /*
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            ViewData["MyBills"] = new SelectList(_billService.GetMyBills(user.Id), "Id", "Amount"); //fix: find a way to show the 'residentName(email)' instead of id
            */
            return View(payment);
        }
        public async Task<IActionResult> MyMessagesAsync()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            return View(_messageService.GetMyMessages(user.Id));
        }
        public void GetCurrentUser(){ } //fix: implement this with using HttpContextAccessor
    }
}
