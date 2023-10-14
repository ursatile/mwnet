using AngleSharp;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace Rockaway.WebApp.Tests.Pages;

public class PageTests {
	[Fact]
	public async Task Index_Page_Returns_Success() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var result = await client.GetAsync("/");
		result.EnsureSuccessStatusCode();
	}

	[Fact]
	public async Task Homepage_Title_Has_Correct_Content() {
		var browsingContext = BrowsingContext.New(Configuration.Default);
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var html = await client.GetStringAsync("/");
		var dom = await browsingContext.OpenAsync(req => req.Content(html));
		var title = dom.QuerySelector("title");
		title.ShouldNotBeNull();
		title.InnerHtml.ShouldBe("Rockaway");
	}

	[Theory]
	[InlineData("/privacy")]
	public async Task Page_Returns_Success(string url) {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var result = await client.GetAsync(url);
		result.EnsureSuccessStatusCode();
	}
}