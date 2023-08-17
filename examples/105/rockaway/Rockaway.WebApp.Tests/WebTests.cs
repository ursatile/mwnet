namespace Rockaway.WebApp.Tests;

public class WebTests {
	[Fact]
	public async Task GET_Status_Returns_OK() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var response = await client.GetAsync("/status");
		response.EnsureSuccessStatusCode();
	}

    [Fact]
    public async Task GET_Status_Includes_ISO3601_DateTime() {
        var testDateTime = new DateTime(2023,4,5,6,7,8);
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
        var testDateTime = new DateTime(2023,4,5,6,7,8);
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
