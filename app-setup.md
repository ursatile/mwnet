---
title: Introduction
layout: home
nav_order: 0000
typora-copy-images-to: ./images
summary: Introduction - why .NET, why C#, choosing a .NET version. The scenario.
---

Monday:

09:00-10:30 Razor Pages, minimal APIs, service registration, testing. **Start the Azure deployment before the coffee break!**

10:45-12:30 Model/View/Controller, our first entity, Areas, plumb in the admin area

13:30-15:00 Style up the frontend, SASS, responsive navigation

15:30-17:00 



In this section, we're going to set up the basic structure of our web and test projects.

MS1: Razor pages, minimal APIs, service registration

First, we'll create a new Razor Pages web application, a new xUnit test project, and a solution to put them in, and create a reference to our web app inside our test project:

```console
dotnet new razor -o Rockaway.WebApp
dotnet new xunit -o Rockaway.WebApp.Tests
dotnet new sln
dotnet sln add Rockaway.WebApp
dotnet sln add Rockaway.WebApp.Tests
dotnet add Rockaway.WebApp.Tests reference Rockaway.WebApp
```

This gives us a basic web app with Razor Pages support.

Add the `.editorconfig`

`dotnet format`

Add Serilog:

```
cd Rockaway.WebApp
dotnet add package Serilog.AspNetCore
```

and in Program.cs

```csharp
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
```

That's enough. Go live. Ship it.

Set up the Azure app. It'll push the `yml` file to the Git repo

Pull it.

Fix the project path, add the unit tests

Watch it go live.

Turn on application insights



How can we test it?

In `Rockaway.WebApp.Tests`:

```console
$ dotnet add package Microsoft.AspNetCore.Mvc.Testing
```

This gives us access to the `WebApplicationFactory`.

Hack the Rockaway.WebApp.csproj to add InternalsVisibleTo:

```xml
	<ItemGroup>
		<InternalsVisibleTo Include="Rockaway.WebApp.Tests" />
	</ItemGroup>
```

(NCrunch: this requires a Visual Studio close & reopen)

Remove UnitTest1

Add `PagesTests.cs`, in the `Pages` namespace:

```csharp
namespace Rockaway.WebApp.Tests.Pages;

public class PagesTests {
	[Fact]
	public async Task Index_Page_Returns_Success() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var result = await client.GetAsync("/");
		result.EnsureSuccessStatusCode();
	}
}
```

Add Shouldly and AngleSharp.

```console
dotnet add package AngleSharp
dotnet add package Shouldly
```

Wire in a test that the `<title`> element of the home page says **Rockaway**

```csharp
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
```



**Service registration and the status endpoint**

We're going to add an endpoint at /status which will return a JSON response containing:

* The full name of the assembly that's hosting our application
* The last modification time of the assembly file (so we know when our app was built)
* The hostname of the server that's running our application
* The current local UTC time on that server

This endpoint should return a JSON document in this format:

```json
{
    "assembly": "Rockaway.WebApp",
    "modified": "2023-10-08T10:15:42Z",
    "hostname": "dylan-windows-pc",
    "datetime": "2023-10-08T12:12:40Z"
}
```

Get it live

**Create a new web app + database setup on Azure**

Exercises

1. Create a Contact Us page at /contact
2. The page title should be "Contact Us" - verify this with a test
3. Create an uptime endpoint, which returns a JSON object showing how long it is since the application was last started or restarted.

## END OF PART ONE

### Part 2: ASP.NET MVC and Entity Framework, DbContext, 

Milestone: /artists - a list of artists

Add MVC support:

```csharp
builder.Services.AddControllersWithViews();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");	
```

Plug in EF Core. We're going to install versions for both SQL Server and Sqlite:

```console
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

Create our first entity:

```csharp
namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
	[MaxLength(500)]
	public string Description { get; set; } = String.Empty;

	[MaxLength(100)]
	[Unicode(false)]
    [RegularExpression("^[a-z0-9-]{2,100}", 
		ErrorMessage = "Slug must be 2-100 characters and contain only a-z, 0-9 and hyphen (-) characters")]
	public string Slug { get; set; } = String.Empty;

	public Artist() { }

	public Artist(Guid id, string name, string description, string slug) {
		this.Id = id;
		this.Name = name;
		this.Description = description;
		this.Slug = slug;
	}
}
```

Create our **DbContext**:

```csharp
using Microsoft.EntityFrameworkCore.Infrastructure;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext : DbContext {
	// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
	// if we want to use Asp.NET to configure our database connection and provider.
	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }

	public DbSet<Artist> Artists { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		// Override EF Core's default table naming (which pluralizes entity names)
		// and use the same names as the C# classes instead
		// and use the same names as the C# classes instead
		var rockawayEntities = modelBuilder.Model.GetEntityTypes().Where(e => e.ClrType.Namespace == typeof(Artist).Namespace);
		foreach (var entity in rockawayEntities) entity.SetTableName(entity.DisplayName());

		modelBuilder.Entity<Artist>(entity => {
			entity.HasIndex(artist => artist.Slug).IsUnique();
		});
	}
}
```

Register the DbContext:

```
var sqlite = new SqliteConnection("Data Source=:memory:");
sqlite.Open();
builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlite(sqlite));
```

Add the code generator:

```dotnetcli
dotnet tool install -g dotnet-aspnet-codegenerator
```

If you run it now, it'll say

```
Building project ...
Scaffolding failed.
Add Microsoft.VisualStudio.Web.CodeGeneration.Design package to the project as a NuGet package reference.
To see more information, enable tracing by setting environment variable 'codegen_trace' = 1.
RunTime 00:00:05.05
```

So we install the package

```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

and this one:

```
 Microsoft.EntityFrameworkCore.Tools
```

Scaffold the ArtistController

```dotnetcli
dotnet aspnet-codegenerator controller -name ArtistsController -m Artist -dc RockawayDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
```

OK, run it.

It'll look like garbage

Move ViewStart and ViewImports into / so they apply to *everything*, not just pages.

Sorted.

### Sample Data

Two problems:

1. We don't have any data.
2. Any data we *do* create will get deleted every time we restart the app.

Solution: Sample Data

* Add the `SampleData.Artist` class
* Add the `SampleData` TestGuid int
* add the HasData

*At this point, we're seeding from the Artist entity directly.*

## Exercise: Add Venues

* Add the Venue.cs class

* Scaffold the VenuesController

* Add Venues to the RockawayDbContext:

  * Make the Slug property unique

  

  

### Going Live: Switching from Sqlite to SQL Server

Wire up the switch of DB providers

```
dotnet run --environment Staging
```



Generate the migration

INspect it

Roll it back

Fix the table naming convention

```csharp
// Override EF Core's default table naming (which pluralizes entity names)
// and use the same names as the C# classes instead
var rockawayEntities = modelBuilder.Model.GetEntityTypes().Where(e => e.ClrType.Namespace == typeof(Artist).Namespace);
foreach (var entity in rockawayEntities) entity.SetTableName(entity.DisplayName());
```

Roll it forward

Commit it









**Exercise**

Let's plug in Venues

Here's the entity class:

```
namespace Rockaway.WebApp.Data.Entities;

public class Venue {

	public Venue() { }

	public Venue(Guid id, string name, string address, string city, string countryCode, string? postalCode, string? telephone, string? websiteUrl) {
		Id = id;
		Name = name;
		Address = address;
		City = city;
		CountryCode = countryCode;
		PostalCode = postalCode;
		Telephone = telephone;
		WebsiteUrl = websiteUrl;
	}

	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	[MaxLength(500)]
	public string Address { get; set; } = String.Empty;

	public string City { get; set; } = String.Empty;

	[Unicode(false)]
	[MaxLength(2)]
	public string CountryCode { get; set; } = String.Empty;

	public string? PostalCode { get; set; }

	[Phone]
	public string? Telephone { get; set; }

	[Url]
	public string? WebsiteUrl { get; set; }

	public string FullName => $"{Name}, {City}, {CountryCode}";
}
```

Here's the sample data for Venues:

```
dotnet aspnet-codegenerator controller -name VenuesController -m Venue -dc RockawayDbContext --useDefaultLayout --referenceScriptLibraries
```

Add custom validation for Country

Plug in the Country class

Add tests for country validation

```
	public async Task CreateVenueWithInvalidCountryCodeReturnsError() {
		var c = new VenuesController(null!);
		var post = new Venue { CountryCode = "XX" };
		var result = await c.Create(post) as ViewResult;
		result.StatusCode.ShouldBe((int)HttpStatusCode.BadRequest);
	}
```



Add test demonstrating how to create country by using an inmemory DB Context:

```
	[Fact]
	public async Task CreateVenueWithValidCountryCreatesCountry() {
		var options = new DbContextOptionsBuilder<RockawayDbContext>().UseSqlite("Data Source=:memory:").Options;
		var db = new RockawayDbContext(options);
		await db.Database.GetDbConnection().OpenAsync();
		await db.Database.EnsureCreatedAsync();
		var c = new VenuesController(db);
		var venue = new Venue {
			Name = "Test Venue", Address = "123 Test Street", City = "Test", CountryCode = "PT",
			Telephone = "1234", WebsiteUrl = "https://example.com"
		};
		await c.Create(venue);
		var created = db.Venues.Single(v => v.Name == "Test Venue");
		created.ShouldBeEquivalentTo(venue);
	}
```

Replace Country input with dropdownlist on the input page:

```
<label asp-for="CountryCode" class="control-label"></label>
<select asp-for="CountryCode" asp-items="Country.AllCountries.Select(c => new SelectListItem(c.Name, c.Code))">
    <option>select...</option>
</select>
<span asp-validation-for="CountryCode" class="text-danger"></span>
```

Get it live on Azure

This means making it work with SQL Server, so we need to generate the migrations

```

```

do a dotnet ef database update:

```console

```

and then build and deploy.

NEXT

We're going to scaffold **identity** and **admin area**

Scaffolding Identity:

[https://learn.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-7.0&tabs=netcore-cli](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-7.0&tabs=netcore-cli)

```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Identity.UI
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

To wire up identity, we actually need to do these things:

1. Make RockawayDbContext inherit from `IdentityDbContext<IdentityUser>`
2. Create `/Pages/Shared/_LoginPartial.cshtml`:

```html
@using Microsoft.AspNetCore.Identity

@inject SignInManager<IdentityUser> SignInManager
@inject UserManager<IdentityUser> UserManager

<ul class="navbar-nav">
@if (SignInManager.IsSignedIn(User))
{
    <li class="nav-item">
        <a id="manage" class="nav-link text-dark" asp-area="Identity" asp-page="/Account/Manage/Index" title="Manage">Hello @UserManager.GetUserName(User)!</a>
    </li>
    <li class="nav-item">
        <form id="logoutForm" class="form-inline" asp-area="Identity" asp-page="/Account/Logout" asp-route-returnUrl="@Url.Page("/Index", new { area = "" })">
            <button id="logout" type="submit" class="nav-link btn btn-link text-dark border-0">Logout</button>
        </form>
    </li>
}
else
{
    <li class="nav-item">
        <a class="nav-link text-dark" id="register" asp-area="Identity" asp-page="/Account/Register">Register</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" id="login" asp-area="Identity" asp-page="/Account/Login">Login</a>
    </li>
}
</ul>

```

Add LoginPartial to _Layout.cshtml

```html
	                </ul>
	                <partial name="_LoginPartial" />
                </div>
            </div>
        </nav>
```



Edit `_ViewImports.cshtml` and add:

```
@using Microsoft.AspNetCore.Identity
```

and finally, in Program.cs

```csharp
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<RockawayDbContext>();

```

That's it. Now we have identity.

To add a sample user:

```
using Microsoft.AspNetCore.Identity;

namespace Rockaway.WebApp.Data.Sample;

public partial class SampleData {

	public static class Users {
		static Users() {
			var hasher = new PasswordHasher<IdentityUser>();
			Admin = new() {
				Id = "rockaway-sample-admin-user",
				Email = "admin@rockaway.dev",
				NormalizedEmail = "admin@rockaway.dev".ToUpperInvariant(),
				UserName = "admin@rockaway.dev",
				NormalizedUserName = "admin@rockaway.dev".ToUpperInvariant(),
				LockoutEnabled = true,
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString()
			};
			Admin.PasswordHash = hasher.HashPassword(Admin, "p@ssw0rd");
		}
		public static IdentityUser Admin { get; }
	}
}
```

and

```
modelBuilder.Entity<IdentityUser>().HasData(Users.Admin);
```

Run it. Boom. It works.

Now, we need to create the migration:

```console
dotnet ef migrations add AddAspNetIdentity
```

OK, done.

Next up: move all the admin stuff into an `/admin` area and secure it.

```
dotnet aspnet-codegenerator area Admin
```

Move the controllers and views

Add the attribute to the controllers:

```
[Area("admin")]
public class VenuesController : Controller {

```

Let's lock it down:

```Authorization/AuthorizationPolicyExtensions.cs`

```
using Microsoft.AspNetCore.Authorization;
namespace Rockaway.WebApp.Authorization;

public static class AuthorizationPolicyExtensions {
	private static AuthorizationPolicy BuildEmailDomainPolicy(string domain) => new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.RequireEmailDomain(domain)
		.Build();

	public static IServiceCollection BuildEmailDomainPolicy(this IServiceCollection services,
		string policyName, string emailDomain) {
		var policy = BuildEmailDomainPolicy("rockaway.dev");
		services.AddAuthorization(options => options.AddPolicy(policyName, policy));
		return services;
	}

	public static AuthorizationPolicyBuilder RequireEmailDomain(this AuthorizationPolicyBuilder builder, string domain) {
		domain = domain.StartsWith("@") ? domain : $"@{domain}";
		return builder.RequireAssertion(ctx => {
			var email = ctx.User?.Identity?.Name ?? "";
			return email.EndsWith(domain, StringComparison.InvariantCultureIgnoreCase);
		});
	}
}
```

Wire up an admin homepage:

```console
dotnet aspnet-codegenerator controller -name Home -namespace Rockaway.WebApp.Areas.Admin.Controllers -outDir Areas/Admin/Controllers
dotnet aspnet-codegenerator view Areas/Admin/Views/Home/Index Empty --useDefaultLayout
```

Add the Area attribute:

```
using Microsoft.AspNetCore.Mvc;

namespace Rockaway.WebApp.Areas.Admin.Controllers; 

[Area("admin")]
public class HomeController : Controller {
	public IActionResult Index() {
		return View();
	}
}
```

Bingo. We have:

a secure Admin area

CRUD operations for artists and venues

What's next?

Let's wire up the rest of our domain model:



/snip

OK, now we need to plug in the admin controller for editing shows.

This is where it gets gnarly.



























