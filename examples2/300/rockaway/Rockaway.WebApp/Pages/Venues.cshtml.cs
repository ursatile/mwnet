using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rockaway.WebApp.Pages;

public class VenuesModel : PageModel {
	private readonly RockawayDbContext db;

	public VenuesModel(RockawayDbContext db) {
		this.db = db;
	}

	public IEnumerable<Venue> Venues { get; set; } = new List<Venue>();

	public void OnGet(string name) {
		this.Venues = db.Venues;
	}
}