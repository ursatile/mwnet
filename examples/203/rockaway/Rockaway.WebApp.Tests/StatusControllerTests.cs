using System.Runtime.InteropServices.ComTypes;

namespace Rockaway.WebApp.Tests;

public class StatusControllerTests {

	[Fact]
	public async Task Status_Index_Returns_Message() {
		var db = await TestDatabase.CreateAsync();
		var testDateTime = new DateTime(2023, 4, 5, 6, 7, 8);
		var clock = new TestClock(testDateTime);
		var c = new StatusController(clock, db);
		var result = c.Index() as ViewResult;
		result.ShouldNotBeNull();
		var model = result.Model as SystemStatus;
		model.ShouldNotBeNull();
		model.Message.ShouldBe("Rockaway.WebApp is online");
	}

	[Fact]
	public async Task Status_Index_Returns_DateTime() {
		var db = await TestDatabase.CreateAsync();
		var testDateTime = new DateTime(2023, 4, 5, 6, 7, 8);
		var clock = new TestClock(testDateTime);
		var c = new StatusController(clock, db);
		var result = c.Index() as ViewResult;
		result.ShouldNotBeNull();
		var model = result.Model as SystemStatus;
		model.ShouldNotBeNull();
		model.SystemTime.ShouldBe(testDateTime);
	}
}