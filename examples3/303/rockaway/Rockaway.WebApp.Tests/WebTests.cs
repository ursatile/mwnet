using System.Net;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Tests;

public class WebTests {

	[Fact]
	public async Task Artist_Page_Shows_Artist_Detail() {
		var artist = SampleData.Artists.DevLeppard;
		var factory = new TestFactory();
		var client = factory.CreateClient();
		var response = await client.GetAsync($"/artist/{artist.Slug}");
		response.EnsureSuccessStatusCode();
		var html = WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync());
		html.ShouldContain(artist.Name);
	}

	[Fact]
	public async Task Artists_Page_Lists_Artists() {
		var clock = new TestClock();
		var factory = new TestFactory(clock);
		var client = factory.CreateClient();
		var response = await client.GetAsync("/artists");
		response.EnsureSuccessStatusCode();
		var html = await response.Content.ReadAsStringAsync();
		html = WebUtility.HtmlDecode(html);
		foreach (var artist in factory.DbContext.Artists) html.ShouldContain(artist.Name);
	}

	[Fact]
	public async Task Venues_Page_Lists_Venues() {
		var clock = new TestClock();
		var factory = new TestFactory(clock);
		var client = factory.CreateClient();
		var response = await client.GetAsync("/venues");
		response.EnsureSuccessStatusCode();
		var html = await response.Content.ReadAsStringAsync();
		html = WebUtility.HtmlDecode(html);
		foreach (var venue in factory.DbContext.Venues) html.ShouldContain(venue.Name);
	}

	[Fact]
	public async Task GET_Status_Returns_OK() {
		var testDateTime = new DateTime(2023, 4, 5, 6, 7, 8);
		var clock = new TestClock(testDateTime);
		var factory = new TestFactory(clock);
		var client = factory.CreateClient();
		var response = await client.GetAsync("/status");
		response.EnsureSuccessStatusCode();
	}

	[Fact]
	public async Task GET_Status_Includes_ISO3601_DateTime() {
		var testDateTime = new DateTime(2023, 4, 5, 6, 7, 8);
		var clock = new TestClock(testDateTime);
		var factory = new TestFactory(clock);
		var client = factory.CreateClient();
		var response = await client.GetAsync("/status");
		var html = await response.Content.ReadAsStringAsync();
		html.ShouldContain(testDateTime.ToString("o"));
	}

	protected IBrowsingContext browsingContext => BrowsingContext.New(Configuration.Default);

	[Fact]
	public async Task GET_Status_Includes_ISO3601_DateTime_Element() {
		var testDateTime = new DateTime(2023, 4, 5, 6, 7, 8);
		var clock = new TestClock(testDateTime);
		var factory = new TestFactory(clock);
		var client = factory.CreateClient();
		var response = await client.GetAsync("/status");
		var html = await response.Content.ReadAsStringAsync();
		var dom = await browsingContext.OpenAsync(req => req.Content(html));
		var element = dom.QuerySelector("#system-time");
		element.ShouldNotBeNull();
		element.InnerHtml.ShouldBe(testDateTime.ToString("o"));
	}
}