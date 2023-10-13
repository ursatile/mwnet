using Rockaway.WebApp.Areas.Admin.Controllers;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Tests.Admin.Controllers;

public class ArtistsControllerTests : IDisposable {

	private readonly TestDatabase tdb;

	public ArtistsControllerTests() {
		tdb = new();
	}

	private RockawayDbContext db => tdb.DbContext;

	[Fact]
	public async void Index_Returns_List_Of_Artists() {
		var c = new ArtistsController(db);
		var result = await c.Index() as ViewResult;
		result.ShouldNotBeNull();
		var artists = (result.Model as IEnumerable<Artist>)?.ToList();
		artists.ShouldNotBeNull();
		artists.Count.ShouldBe(db.Artists.Count());
	}

	[Fact]
	public async void Detail_Shows_Specific_Artist() {
		var a = SampleData.Artists.DevLeppard;
		var c = new ArtistsController(db);
		var result = await c.Details(a.Id) as ViewResult;
		result.ShouldNotBeNull();
		var artist = result.Model as Artist;
		artist.ShouldBeEquivalentTo(a);
	}

	[Fact]
	public async void Create_Adds_New_Artist() {
		var c = new ArtistsController(db);
		var artist = new Artist() {
			Name = "Test Name",
			Description = "Test Description",
			Slug = "test-slug"
		};
		await c.Create(artist);
		var record = db.Artists.Single(a => a.Slug == "test-slug");
		record.ShouldBeEquivalentTo(artist);
	}

	[Fact]
	public async void Update_Updates_Artist() {
		var c = new ArtistsController(db);
		var artist = SampleData.Artists.DevLeppard;
		artist.Name = "Test name";
		artist.Description = "Test description";
		artist.Slug = "test-slug";
		await c.Edit(artist.Id, artist);
		var record = db.Artists.Single(a => a.Slug == "test-slug");
		record.ShouldBeEquivalentTo(artist);
	}

	[Fact]
	public async void Delete_Deletes_Artist() {
		var c = new ArtistsController(db);
		var artist = SampleData.Artists.DevLeppard;
		await c.DeleteConfirmed(artist.Id);
		var record = await db.Artists.FindAsync(artist.Id);
		record.ShouldBe(default);
	}

	public void Dispose() {
		tdb.Dispose();
	}
}