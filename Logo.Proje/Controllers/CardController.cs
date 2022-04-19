using Logo.Proje.Business.Abstracts;
using Logo.Proje.Data;
using Logo.Proje.Domain.MongoDbEntities;
using Logo.Proje.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Logo.Proje.Controllers
{
    public class CardController : Controller
    {
        private readonly ApplicationDbContext _userContext;
        private readonly ICardService _cardService;
        private readonly IMongoCollection<Card> _cards;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public CardController(ICardService cardService,
            ApplicationDbContext usercontext,
            IMongoClient mongoClient,
            UserManager<CustomIdentityUser> userManager)
        {
            _userContext = usercontext;
            _cardService = cardService;
            _cards = mongoClient.GetDatabase("LogoDb").GetCollection<Card>("cards");
            _userManager = userManager;

        }

        // GET: Card
        public IActionResult Index()
        {
            return View(_cardService.GetAllCards());
        }

        // GET: Card/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _cardService.GetCardById(x => x.Id == id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // GET: Card/Create
        public async Task<IActionResult> CreateAsync()
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
        public IActionResult Create([Bind("CardName,OwnerId,OwnerFullName,CardNumber,ExpirationDate,CVV,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Card card)
        {
            if (ModelState.IsValid)
            {
                _cardService.AddCard(card);
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_userContext.Users, "Id", "Email"); //fix: find a way to show the 'fullname(email)' instead of just email
            return View(card);
        }

        // GET: Card/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var card = _cardService.GetCardById(x => x.Id == id);
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
        public IActionResult Edit(string id, [Bind("CardName,OwnerId,OwnerFullName,CardNumber,ExpirationDate,CVV,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _cardService.UpdateCard(card);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Id))
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
            return View(card);
        }

        // GET: Card/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var card = _cardService.GetCardById(x => x.Id == id);
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
            _cardService.DeleteCard(new Card { Id = id });
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(string id)
        {
            return _cards.Find<Card>(e => e.Id == id).Any();
        }
    }
}
