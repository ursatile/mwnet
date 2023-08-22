using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rockaway.WebApp.Pages;

public class ArtistModel : PageModel {
	private readonly RockawayDbContext db;

	public ArtistModel(RockawayDbContext db) {
		this.db = db;
	}

	public Artist Artist { get; set; } = null!;

	public void OnGet(string slug) {
		this.Artist = db.Artists.Single(a => a.Name == slug);
	}
}