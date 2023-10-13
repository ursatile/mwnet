using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Pages;

public class ArtistsModel : PageModel {
	private readonly ILogger<IndexModel> logger;
	private readonly RockawayDbContext db;
	public IEnumerable<Artist> Artists = default!;

	public ArtistsModel(RockawayDbContext db, ILogger<IndexModel> logger) {
		this.db = db;
		this.logger = logger;
	}

	public void OnGet() {
		Artists = db.Artists.OrderBy(a => a.Name);
	}
}