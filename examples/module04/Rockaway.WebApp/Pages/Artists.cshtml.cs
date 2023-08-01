using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Pages;

public class ArtistsPageModel : PageModel {
	private readonly RockawayDbContext dbc;

	public ArtistsPageModel(RockawayDbContext dbc) {
		this.dbc = dbc;
	}

	public IList<Artist> Artists { get; set; } = new List<Artist>();

	public async Task OnGetAsync() {
		Artists = await dbc.Artists.ToListAsync();
	}
}