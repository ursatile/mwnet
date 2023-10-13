namespace Rockaway.WebApp.Tests.WebTests;

public class AdminTests {

	[Fact]
	public async Task Create_Artist_Creates_New_Artist() {
		const string URL = "/admin/artists/create";
		var factory = new TestFactory();
		var client = factory.WithFakeAuth().CreateClient();
		var token = await client.GetAntiForgeryTokenAsync(URL);
		var request = new HttpRequestMessage(HttpMethod.Post, URL);
		token.AddCookie(request);

		var fields = new Dictionary<string, string> {
			{ "Id", Guid.Empty.ToString() },
			{ "Name", "Edited Name" },
			{ "Description", "Edited Description" },
			{ "Slug", "edited-slug" }
		};
		token.AddField(fields);
		request.Content = new FormUrlEncodedContent(fields);

		var response = await client.SendAsync(request);

		response.EnsureSuccessStatusCode();

		var updated = await factory.DbContext.Artists
			.SingleOrDefaultAsync(a => a.Slug == "edited-slug");
		updated.ShouldNotBeNull();
		updated.Name.ShouldBe("Edited Name");
		updated.Description.ShouldBe("Edited Description");
	}
}