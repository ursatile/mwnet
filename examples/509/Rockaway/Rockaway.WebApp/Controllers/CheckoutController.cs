using Rockaway.WebApp.Data;

namespace Rockaway.WebApp.Controllers;

public class CheckoutController(RockawayDbContext db,
	ILogger<CheckoutController> logger) : Controller {

	public async Task<IActionResult> Details(Guid id) {
		var ticketOrder = await db.TicketOrders
			.Include(o => o.Contents)
			.ThenInclude(c => c.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.Venue)
			.Include(o => o.Contents)
			.ThenInclude(c => c.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.HeadlineArtist)
			.Include(o => o.Contents)
			.ThenInclude(c => c.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.SupportSlots).ThenInclude(ss => ss.Artist)
			.FirstOrDefaultAsync(order => order.Id == id);
		if (ticketOrder == default) return NotFound();
		return Ok(id);
	}
}

