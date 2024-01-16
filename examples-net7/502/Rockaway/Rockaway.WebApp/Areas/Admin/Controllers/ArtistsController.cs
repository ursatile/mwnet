using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Areas.Admin.Controllers {
	[Area("admin")]
	public class ArtistsController : Controller {
		private readonly RockawayDbContext db;

		public ArtistsController(RockawayDbContext db) {
			this.db = db;
		}

		// GET: Artists
		public async Task<IActionResult> Index() {
			return db.Artists != null ?
						View(await db.Artists.ToListAsync()) :
						Problem("Entity set 'RockawayDbContext.Artists'  is null.");
		}

		// GET: Artists/Details/5
		public async Task<IActionResult> Details(Guid? id) {
			if (id == null || db.Artists == null) {
				return NotFound();
			}

			var artist = await db.Artists
				.FirstOrDefaultAsync(m => m.Id == id);
			if (artist == null) {
				return NotFound();
			}

			return View(artist);
		}

		// GET: Artists/Create
		public IActionResult Create() {
			return View();
		}

		// POST: Artists/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Description,Slug")] Artist artist) {
			if (ModelState.IsValid) {
				artist.Id = Guid.NewGuid();
				db.Add(artist);
				await db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(artist);
		}

		// GET: Artists/Edit/5
		public async Task<IActionResult> Edit(Guid? id) {
			if (id == null || db.Artists == null) {
				return NotFound();
			}

			var artist = await db.Artists.FindAsync(id);
			if (artist == null) {
				return NotFound();
			}
			return View(artist);
		}

		// POST: Artists/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Slug")] Artist artist) {
			if (id != artist.Id) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					db.Update(artist);
					await db.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException) {
					if (!ArtistExists(artist.Id)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(artist);
		}

		// GET: Artists/Delete/5
		public async Task<IActionResult> Delete(Guid? id) {
			if (id == null || db.Artists == null) {
				return NotFound();
			}

			var artist = await db.Artists
				.FirstOrDefaultAsync(m => m.Id == id);
			if (artist == null) {
				return NotFound();
			}

			return View(artist);
		}

		// POST: Artists/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id) {
			if (db.Artists == null) {
				return Problem("Entity set 'RockawayDbContext.Artists'  is null.");
			}
			var artist = await db.Artists.FindAsync(id);
			if (artist != null) {
				db.Artists.Remove(artist);
			}

			await db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ArtistExists(Guid id) {
			return (db.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}