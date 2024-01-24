using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rockaway.WebApp.Controllers;

namespace Rockaway.WebApp.Tests;

public class TicketTests {
	[Fact]
	public async Task Buying_Tickets_Creates_Order() {
		var loggerFactory = LoggerFactory.Create(logBuilder => logBuilder.AddConsole());
		var dbName = Guid.NewGuid().ToString();
		var db = TestDatabase.Create(dbName);

		var c = new TicketsController(db, loggerFactory.CreateLogger<TicketsController>());
		var show = await db.Shows.Include(s => s.Venue).FirstOrDefaultAsync();
		show.ShouldNotBeNull();
		show.TicketOrders.ShouldBeEmpty();
		var tickets = show.TicketTypes.ToDictionary(tt => tt.Id, _ => 1);
		var howManyTicketsToExpect = tickets.Count;
		await c.Show(show.Venue.Slug, show.Date, tickets);

		var db2 = TestDatabase.Connect(dbName);
		db2.TicketOrders.Count().ShouldBe(1);
		db2.TicketOrders.First().Contents.Count.ShouldBe(howManyTicketsToExpect);
	}
}
