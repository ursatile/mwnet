using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Rockaway.WebApp.Data;

namespace Rockaway.WebApp.Controllers {
	public class TicketsController(RockawayDbContext db, ILogger<TicketsController> logger) : Controller {
		[Route("show/{venue}/{date}")]
		public IActionResult Show(string venue, LocalDate date) {
			var show = db.Shows
				.Include(s => s.TicketTypes)
				.Include(s => s.Venue)
				.Include(s => s.HeadlineArtist)
				.Include(s => s.SupportSlots).ThenInclude(slot => slot.Artist)
				.FirstOrDefault(s => s.Venue.Slug == venue && s.Date == date);

			return View(show);
		}
	}
}
