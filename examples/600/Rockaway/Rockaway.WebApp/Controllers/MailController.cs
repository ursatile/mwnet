using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Models;
using Rockaway.WebApp.Services;
using Rockaway.WebApp.Services.Mail;

namespace Rockaway.WebApp.Controllers;

public class MailController(RockawayDbContext db, IMailBodyRenderer renderer) : Controller {
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

	public async Task<IActionResult> HtmlMail(Guid id) {
		var ticketOrder = await FindOrderAsync(id);
		if (ticketOrder == default) return NotFound();

		// ReSharper disable once InvokeAsExtensionMethod
		var uri = UriExtensions.GetWebsiteBaseUri(Request);
		var data = new TicketOrderMailData(ticketOrder, uri);
		var html = renderer.RenderOrderConfirmationHtml(data);
		return Content(html, "text/html");
	}
}
