using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Logo.Proje.DataAccess.EntityFramework;
using Logo.Proje.Domain.Entities;
using Logo.Proje.Business.Abstracts;
using Logo.Proje.Data;
using Microsoft.AspNetCore.Authorization;

namespace Logo.Proje.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ApplicationDbContext _userContext;
        private readonly IMessageService _messageService;

        public MessageController(AppDbContext context, ApplicationDbContext userContext, IMessageService messageService)
        {
            _context = context;
            _userContext = userContext;
            _messageService = messageService;
        }

        // GET: Message
        public IActionResult Index()
        {
            return View(_messageService.GetAllMessages());
        }

        // GET: Message/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var message = _messageService.GetMessageById(x => x.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Message/Create
        public IActionResult Create()
        {
            ViewData["To"] = new SelectList(_userContext.Users, "Id", "Email"); //fix: find a way to show the 'fullName(email)' instead of email
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("From,To,Text,IsRead,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Message message)
        {
            if (ModelState.IsValid)
            {
                _messageService.AddMessage(message);
                return RedirectToAction(nameof(Index));
            }
            ViewData["To"] = new SelectList(_userContext.Users, "Id", "Email"); //fix: find a way to show the 'residentName(email)' instead of email
            return View(message);
        }

        // GET: Message/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var message = _messageService.GetMessageById(x => x.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["To"] = new SelectList(_userContext.Users, "Id", "Email", message.To); //fix: find a way to show the 'residentName(email)' instead of email
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("From,To,Text,IsRead,Id,IsDeleted,CreatedAt,CreatedBy,LastUpdatedAt,LastUpdatedBy")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _messageService.UpdateMessage(message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Id))
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
            ViewData["To"] = new SelectList(_userContext.Users, "Id", "Email", message.To); //fix: find a way to show the 'residentName(email)' instead of email
            return View(message);
        }

        // GET: Message/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var message = _messageService.GetMessageById(x => x.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _messageService.DeleteMessage(new Message { Id = id, LastUpdatedBy = "SYSTEM" });
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
