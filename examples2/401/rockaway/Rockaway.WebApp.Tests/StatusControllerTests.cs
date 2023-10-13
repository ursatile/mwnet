using System.Runtime.InteropServices.ComTypes;

namespace Rockaway.WebApp.Tests;

public class StatusControllerTests {

	[Fact]
	public void Shows_Work() {
		var dl = SampleData.Artists.MottTheTuple;
		using var db = new TestDatabase();
		var dt = db.DbContext.Artists.Find(dl.Id);
		var venue = db.DbContext.Venues.First();
		var artist = db.DbContext.Artists.Find(dl.Id);
		var show = venue
			.BookShow(artist, new(2023, 09, 04))
			.WithSupportActs(
				db.DbContext.Artists.Find(SampleData.Artists.JavasCrypt.Id),
				db.DbContext.Artists.Find(SampleData.Artists.PascalsWager.Id));
		db.DbContext.Shows.Add(show);
		db.DbContext.SaveChanges();
		db.DbContext.Shows.Count().ShouldBe(3);
	}


	[Fact]
	public void Status_Index_Returns_Message() {
		using var db = new TestDatabase();
		var testDateTime = new DateTime(2023, 4, 5, 6, 7, 8);
		var clock = new TestClock(testDateTime);
		var c = new StatusController(clock, db.DbContext);
		var result = c.Index() as ViewResult;
		result.ShouldNotBeNull();
		var model = result.Model as SystemStatus;
		model.ShouldNotBeNull();
		model.Message.ShouldBe("Rockaway.WebApp is online");
	}

	[Fact]
	public void Status_Index_Returns_DateTime() {
		using var db = new TestDatabase();
		var testDateTime = new DateTime(2023, 4, 5, 6, 7, 8);
		var clock = new TestClock(testDateTime);
		var c = new StatusController(clock, db.DbContext);
		var result = c.Index() as ViewResult;
		result.ShouldNotBeNull();
		var model = result.Model as SystemStatus;
		model.ShouldNotBeNull();
		model.SystemTime.ShouldBe(testDateTime);
	}
}