using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Models;

namespace Rockaway.WebApp.Controllers;

public class CheckoutController(RockawayDbContext db,
	ILogger<CheckoutController> logger) : Controller {

	private async Task<TicketOrder?> FindOrderAsync(Guid id) {
		return await db.TicketOrders
			.Include(o => o.Contents)
			.ThenInclude(c => c.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.Venue)
			.Include(o => o.Contents)
			.ThenInclude(c => c.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.HeadlineArtist)
			.Include(o => o.Contents)
			.ThenInclude(c => c.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.SupportSlots).ThenInclude(ss => ss.Artist)
			.FirstOrDefaultAsync(order => order.Id == id);
	}

	[HttpPost]
	public async Task<IActionResult> Confirm(OrderConfirmationPostData post) {
		var ticketOrder = await FindOrderAsync(post.TicketOrderId);
		if (ticketOrder == default) return NotFound();
		post.TicketOrder = new(ticketOrder);
		if (ModelState.IsValid) return Ok(post);
		return View(post);
	}

	[HttpGet]
	public async Task<IActionResult> Confirm(Guid id) {
		var ticketOrder = await FindOrderAsync(id);
		if (ticketOrder == default) return NotFound();
		var model = new OrderConfirmationPostData() {
			TicketOrderId = id,
			TicketOrder = new(ticketOrder)
		};
		return View(model);
	}
}