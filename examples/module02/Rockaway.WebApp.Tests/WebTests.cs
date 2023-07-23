namespace Rockaway.WebApp.Tests;

public class WebTests {
	[Fact]
	public async Task GET_Root_Returns_OK() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var response = await client.GetAsync("/");
		response.EnsureSuccessStatusCode();
	}

	[Fact]
	public async Task GET_Artists_Returns_OK() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var response = await client.GetAsync("/artists");
		response.EnsureSuccessStatusCode();
	}
}
