using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Areas.Admin.Controllers; 

[Area("Admin")]
[Authorize]
public class ShowsController : Controller {
	private readonly RockawayDbContext db;

	public ShowsController(RockawayDbContext db) {
		this.db = db;
	}

	// GET: Admin/Shows
	public async Task<IActionResult> Index() {
		return View(await db.Shows.ToListAsync());
	}

	// GET: Admin/Shows/Details/5
	public async Task<IActionResult> Details(Guid venueId, LocalDate date)
		=> Json(new { VenueId = venueId, date });

	//// GET: Admin/Shows/Create
	//public IActionResult Create() {
	//	return View();
	//}

	//// POST: Admin/Shows/Create
	//// To protect from overposting attacks, enable the specific properties you want to bind to.
	//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	//[HttpPost]
	//[ValidateAntiForgeryToken]
	//public async Task<IActionResult> Create([Bind("Date,DoorsOpen,ShowBegins,ShowEnds,SalesBegin")] Show show) {
	//	if (ModelState.IsValid) {
	//		db.Add(show);
	//		await db.SaveChangesAsync();
	//		return RedirectToAction(nameof(Index));
	//	}
	//	return View(show);
	//}

	//// GET: Admin/Shows/Edit/5
	//public async Task<IActionResult> Edit(Guid? id) {
	//	if (id == null || db.Shows == null) {
	//		return NotFound();
	//	}

	//	var show = await db.Shows.FindAsync(id);
	//	if (show == null) {
	//		return NotFound();
	//	}
	//	return View(show);
	//}

	//// POST: Admin/Shows/Edit/5
	//// To protect from overposting attacks, enable the specific properties you want to bind to.
	//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	//[HttpPost]
	//[ValidateAntiForgeryToken]
	//public async Task<IActionResult> Edit(Guid? id, [Bind("Date,DoorsOpen,ShowBegins,ShowEnds,SalesBegin")] Show show) {
	//	if (id != show.VenueId) {
	//		return NotFound();
	//	}

	//	if (ModelState.IsValid) {
	//		try {
	//			db.Update(show);
	//			await db.SaveChangesAsync();
	//		}
	//		catch (DbUpdateConcurrencyException) {
	//			if (!ShowExists(show.VenueId)) {
	//				return NotFound();
	//			} else {
	//				throw;
	//			}
	//		}
	//		return RedirectToAction(nameof(Index));
	//	}
	//	return View(show);
	//}

	//// GET: Admin/Shows/Delete/5
	//public async Task<IActionResult> Delete(Guid? id) {
	//	if (id == null || db.Shows == null) {
	//		return NotFound();
	//	}

	//	var show = await db.Shows
	//		.FirstOrDefaultAsync(m => m.VenueId == id);
	//	if (show == null) {
	//		return NotFound();
	//	}

	//	return View(show);
	//}

	//// POST: Admin/Shows/Delete/5
	//[HttpPost, ActionName("Delete")]
	//[ValidateAntiForgeryToken]
	//public async Task<IActionResult> DeleteConfirmed(Guid? id) {
	//	if (db.Shows == null) {
	//		return Problem("Entity set 'RockawayDbContext.Shows'  is null.");
	//	}
	//	var show = await db.Shows.FindAsync(id);
	//	if (show != null) {
	//		db.Shows.Remove(show);
	//	}

	//	await db.SaveChangesAsync();
	//	return RedirectToAction(nameof(Index));
	//}

	//private bool ShowExists(Guid? id) {
	//	return (db.Shows?.Any(e => e.VenueId == id)).GetValueOrDefault();
	//}
}