using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
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
		var shows = await db.Shows
			.Include(show => show.Headliner)
			.Include(show => show.SupportActs).ThenInclude(slot => slot.Artist)
			.Include(show => show.Venue)
			.ToListAsync();
		return View(shows);
	}

	// GET: Admin/Shows/Details/5
	public async Task<IActionResult> Details(Guid venueId, LocalDate date)
		=> Json(new { VenueId = venueId, date });
}