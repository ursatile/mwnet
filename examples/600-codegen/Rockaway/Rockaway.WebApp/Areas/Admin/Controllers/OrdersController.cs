using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Areas_Admin_Controllers
{
    public class OrdersController : Controller
    {
        private readonly RockawayDbContext _context;

        public OrdersController(RockawayDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketOrders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOrder = await _context.TicketOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketOrder == null)
            {
                return NotFound();
            }

            return View(ticketOrder);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,CustomerEmail,CommencedAt,CompletedAt")] TicketOrder ticketOrder)
        {
            if (ModelState.IsValid)
            {
                ticketOrder.Id = Guid.NewGuid();
                _context.Add(ticketOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketOrder);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOrder = await _context.TicketOrders.FindAsync(id);
            if (ticketOrder == null)
            {
                return NotFound();
            }
            return View(ticketOrder);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CustomerName,CustomerEmail,CommencedAt,CompletedAt")] TicketOrder ticketOrder)
        {
            if (id != ticketOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketOrderExists(ticketOrder.Id))
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
            return View(ticketOrder);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOrder = await _context.TicketOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketOrder == null)
            {
                return NotFound();
            }

            return View(ticketOrder);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ticketOrder = await _context.TicketOrders.FindAsync(id);
            if (ticketOrder != null)
            {
                _context.TicketOrders.Remove(ticketOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketOrderExists(Guid id)
        {
            return _context.TicketOrders.Any(e => e.Id == id);
        }
    }
}