---
title: 3.3 Partials and Tag Helpers
layout: module
nav_order: 0303
summary: >-
  In this module, we'll learn how to use Razor features like partial views and tag helpers to create reusable elements that we can use throughout our ASPNET Core web apps. 
examples: examples/303/rockaway/
typora-root-url: .
typora-copy-images-to: ./images
previous: mwnet302
complete: mwnet303
---

OK, time to make our Artists listing page look good.

We're going to create a **DisplayTemplate** that we can use wherever we want to display information about an Artist:

**Rockaway.WebApp/Views/Shared/DisplayTemplates/Artist.cshtml:**

```html
{% include_relative {{ page.examples}}Rockaway.WebApp/Views/Shared/DisplayTemplates/Artist.cshtml %}
```

Simple, but it works.

Next, we're going to update our Artist page to use this display template:

**Rockaway.WebApp/Pages/Artists.cshtml**

```html
@page
@model Rockaway.WebApp.Pages.ArtistsModel

@foreach (var artist in Model.Artists) {
	<section>
	@Html.DisplayFor(_ => artist)
	</section>
}
```

Now, we want to add a link to each `<section>` so visitors can see more details about the specified artist.

We could use the artist `Id` for this, but then we're going to get URLs like:

`/artists/728d24d5-2448-484c-b434-db8b7ec56c27`

I don't like that, and neither do search engines.

Instead, we're going to add a property called a `slug`, which is a human-readable string version of an artist's name that we can use in URLs.

### **Adding new properties to domain entities**

Let's add the Slug property to our Artist entity. There's a five-step formula for adding new properties to an existing domain entity:

1. Add the new property to the class
2. Define any required indexes or constraints in the `RockawayDbContext` 
3. Provide values for the new field in your sample data fixtures
4. Create a DB migration
5. Apply the DB migration

> No, you can't calculate slugs automatically. In some cases, you can - `Java's Crypt` => `javas-crypt` is pretty simple. But how would you slug `Океан Ельзи` or `Χαΐνηδες`? Or the band called `...` ?
>
> Because our data model supports names containing Unicode characters which won't translate cleanly into a URL, you need to do it manually. What we'll do in a future module is look at some client-side JavaScript to create a sensible default when adding new artists to the system, but for this kind of thing, **a human must decide whether a slug is correct**

Here's `Artist.cs` with the new property added:

**Rockaway.WebApp/Data/Entities/Artist.cs:**

```csharp
{% include_relative {{ page.examples}}Rockaway.WebApp/Data/Entities/Artist.cs %}
```

We're going to define the `slug` property as an index, that when we look up an artist based on a slug value the database can jump straight to the associated record instead of having to go through every record in the table. We do this from the `OnModelCreating` override in `RockawayDbContext`, and we also define the index as `IsUnique` so if we try to insert two artists with the same slug, we'll get a database exception:

```csharp
protected override void OnModelCreating(ModelBuilder builder) {
    builder.Entity<Artist>(artist => {
        artist.HasData(SampleData.Artists.AllArtists);
        artist.HasIndex(a => a.Slug).IsUnique();
    });
    /* other entities here... */
}
```

Adding the new field to our sample data is tedious, but it's necessary. Once you give up on maintaining sample data, your life will get very bad very fast.

**Rockaway.WebApp/Data/Sample/SampleData.cs:**

```csharp
{% include_relative {{ page.examples}}Rockaway.WebApp/Data/Sample/SampleData.cs %}}
```

Once we've updated our entity, our DbContext and our sample data, we can generate the database migration:

```
dotnet ef migrations add AddSlugToArtist
```

**Rockaway.WebApp/Migrations/20230822142034_AddSlugToArtist.cs:**

```csharp
{% include_relative {{page.examples}}Rockaway.WebApp/Migrations/20230822142034_AddSlugToArtist.cs %}
```

Apply the migration:

```csharp
dotnet ef database update
```

> Remember, we can also run our app using the local SQLite database by calling
>
> ```
> dotnet run database=sqlite
> ```

## Creating the Artist detail page

Let's create a new page that will show details for a specific artist, identified by the URL slug

* `/artist/javas-crypt` should show information about Java's Crypt
* `/artist/no-such-band` should return a 404 `Not Found` page

> Note that the profile page is `artist` (singular), not `artists` (plural). With the model/view/controller pattern, it's common to see a listing page as `/products/` and a detail page at `/products/1234`, since logically the `ProductsController` handles both routes; with the pages model, it makes more sense to use plural for collections and singular for details.

First, we'll create a test for our new page. Add this code to `WebTests.cs`:

```csharp
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
```

Now we can update our Artists page to include a link containing the URL slug:

**Rockaway.WebApp/Pages/Artists.cshtml**

```html
@page
@model Rockaway.WebApp.Pages.ArtistsModel

@foreach (var artist in Model.Artists) {
	<section>
	@Html.DisplayFor(_ => artist)
	<a asp-page="Artist" asp-route-slug="@artist.Slug">more info...</a>
	</section>
}
```

and create an artist details page, `Artist.cshtml`, which will display details for the artist matching the specified slug:

**Rockaway.WebApp/Pages/Artist.cshtml**

```html
@page "{slug?}"
@model Rockaway.WebApp.Pages.ArtistModel

@Html.DisplayFor(_ => Model.Artist)
```

**Rockaway.WebApp/Pages/Artist.cshtml.cs**

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rockaway.WebApp.Pages;

public class ArtistModel : PageModel {
	private readonly RockawayDbContext db;

	public ArtistModel(RockawayDbContext db) {
		this.db = db;
	}

	public Artist Artist { get; set; } = null!;

	public void OnGet(string slug) {
		this.Artist = db.Artists.Single(a => a.Slug == slug);
	}
}
```

While we're here, there's one more thing I don't like. Our `Artists` page uses ASP.NET Core's attribute helpers to generate the page link:

```html
<a asp-page="Artist" asp-route-slug="@artist.Slug">more info...</a>
```

By default, that'll generate mixed-case URLs. I don't like mixed-case URLs, so let's override that behaviour.

In our `Program.cs`, right at the top of the file, add a call to `Services.Configure<RouteOptions>`:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
```

That'll cause ASP.NET Core to generate lowercase URLs whenever we use helper methods like `asp-page`, `asp-controller`, `asp-route-slug` and so on.



