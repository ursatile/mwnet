using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Areas.Admin.Controllers {

	[Area("admin")]
	public class VenuesController : Controller {
		private readonly RockawayDbContext db;

		public VenuesController(RockawayDbContext db) {
			this.db = db;
		}

		// GET: Venues
		public async Task<IActionResult> Index() {
			return db.Venues != null ?
						View(await db.Venues.ToListAsync()) :
						Problem("Entity set 'RockawayDbContext.Venues'  is null.");
		}

		// GET: Venues/Details/5
		public async Task<IActionResult> Details(Guid? id) {
			if (id == null || db.Venues == null) {
				return NotFound();
			}

			var venue = await db.Venues
				.FirstOrDefaultAsync(m => m.Id == id);
			if (venue == null) {
				return NotFound();
			}

			return View(venue);
		}

		// GET: Venues/Create
		public IActionResult Create() {
			return View();
		}

		// POST: Venues/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Slug,Address,City,CountryCode,PostalCode,Telephone,WebsiteUrl")] Venue venue) {
			if (ModelState.IsValid) {
				venue.Id = Guid.NewGuid();
				db.Add(venue);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(venue);
		}

		// GET: Venues/Edit/5
		public async Task<IActionResult> Edit(Guid? id) {
			if (id == null || db.Venues == null) {
				return NotFound();
			}

			var venue = await db.Venues.FindAsync(id);
			if (venue == null) {
				return NotFound();
			}
			return View(venue);
		}

		// POST: Venues/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Slug,Address,City,CountryCode,PostalCode,Telephone,WebsiteUrl")] Venue venue) {
			if (id != venue.Id) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					db.Update(venue);
					await db.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException) {
					if (!VenueExists(venue.Id)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(venue);
		}

		// GET: Venues/Delete/5
		public async Task<IActionResult> Delete(Guid? id) {
			if (id == null || db.Venues == null) {
				return NotFound();
			}

			var venue = await db.Venues
				.FirstOrDefaultAsync(m => m.Id == id);
			if (venue == null) {
				return NotFound();
			}

			return View(venue);
		}

		// POST: Venues/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id) {
			if (db.Venues == null) {
				return Problem("Entity set 'RockawayDbContext.Venues'  is null.");
			}
			var venue = await db.Venues.FindAsync(id);
			if (venue != null) {
				db.Venues.Remove(venue);
			}

			await db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool VenueExists(Guid id) {
			return (db.Venues?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}