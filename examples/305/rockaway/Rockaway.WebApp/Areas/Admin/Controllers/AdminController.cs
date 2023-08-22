using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rockaway.WebApp.Areas.Admin.Controllers {
	[Area("admin")]
	[Authorize]
	public class AdminController : Controller {
		private readonly RockawayDbContext db;
		private readonly ILogger<AdminController> logger;

		public AdminController(RockawayDbContext db, ILogger<AdminController> logger) {
			this.db = db;
			this.logger = logger;
		}
		public IActionResult Index() {
			return View();
		}

		public async Task<IActionResult> Artists() {
			var artists = await db.Artists.ToListAsync();
			return View(artists);
		}

		[HttpGet]
		public async Task<IActionResult> Artist(Guid? id = default) {
			var model = await db.Artists.FindAsync(id);
			if (model == default) return NotFound();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Artist(Guid id, Artist post) {
			var artist = await db.Artists.FindAsync(id);
			if (artist == default) return NotFound();
			if (ModelState.IsValid) return View(artist);
			artist.Name = post.Name;
			artist.Description = post.Description;
			artist.Slug = post.Slug;
			await db.SaveChangesAsync();
			return RedirectToAction(nameof(Artist), new { id = artist.Id });
		}
	}
}