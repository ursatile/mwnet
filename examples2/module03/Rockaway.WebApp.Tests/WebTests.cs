using Newtonsoft.Json;
using Rockaway.WebApp.Data.Entities;
using Shouldly;

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
		// Arrange
		var factory = new TestFactory();
		var artist = new Artist { Name = "Test Artist" }; 
		factory.DbContext.Artists.Add(artist);
		await factory.DbContext.SaveChangesAsync();

		// Act
		var client = factory.CreateClient();
		var response = await client.GetAsync("/artists");

		// Assert
		var json = await response.Content.ReadAsStringAsync();
		var data = JsonConvert.DeserializeObject<List<Artist>>(json);
		var result = data.Single();
		result.Name.ShouldBe("Test Artist");
	}
}