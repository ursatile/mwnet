---
title: 3.5 The Admin Area
layout: module
nav_order: 0305
summary: >-
  In this module, we'll create an admin area for our site, add forms to create and update Artists and Venues, and look at how ASP.NET Core and EF Core handle things like validating user input.
typora-root-url: .
typora-copy-images-to: ./images
examples: examples/305/rockaway/
previous: mwnet304
complete: mwnet305
---

We're going to create an **area** for all our admin operations.

> **Areas** in ASP.NET Core give us a way to organise our project structure without having to deal with the overhead of building multiple projects.

For this sectiond, we're going to use the `dotnet-aspnet-codegenerator` tool:

```dotnetcli
dotnet tool install -g dotnet-aspnet-codegenerator
```

We can use the tool to create the area structure:

```
dotnet-aspnet-codegenerator area Admin
```

and then to set up the ArtistsController and the associated views. Note that each line here ends with a backtick `` `  character; in Powershell (and the windows `cmd.exe` interpreter) this escapes the line-break so we can wrap long commands onto multiple lines:

```
dotnet aspnet-codegenerator controller `
	-name ArtistsController `
	--relativeFolderPath Areas\Admin\Controllers `
	--model Artist `
	--dataContext RockawayDbContext `
	--useDefaultLayout
```

> Scaffolded code like this is great for getting something up and running in a hurry. There are no tests around any of the code we just spun up, and the HTML markup is brittle; it creates individual inputs for each of our entity properties, so making any changes to our model could mean updating multiple files. 
>
> That said, it's a great way to quickly create enough structure for us to begin adding and editing records.

This spits out a whole bunch of code that should give us this structure:

```
Rockaway.WebApp
	/Areas
		/Admin
			/Controllers
				ArtistsController.cs
			/Data
			/Models
			/Views
				/Artists
					Create.cshtml
					Delete.cshtml
					Details.cshtml
					Edit.cshtml
					Index.cshtml
```

A few more things to plug in.

### Create an admin homepage

Create a new controller in `/Areas/Admin/Controllers/HomeController.cs`

**Rockaway.WebApp/Areas/Admin/Controllers/HomeController.cs**

```csharp
{% include_relative {{page.examples}}Rockaway.WebApp/Areas/Admin/Controllers/HomeController.cs %}
```

Create the `Home/Index` view:

**Rockaway.WebApp/Areas/Admin/Views/Home/Index.cshtml**

```csharp
{% include_relative {{page.examples}}Rockaway.WebApp/Areas/Admin/Views/Home/Index.cshtml %}
```

Finally -- and this is important -- you need to add the `[Authorize]` attribute to the top of `ArtistsController`, otherwise you'll leave your admin pages wide open and end up in one of Troy Hunt's YouTube videos.

```csharp
namespace Rockaway.WebApp.Areas.Admin.Controllers {
	[Area("Admin")]
	[Authorize]
	public class ArtistsController : Controller {
```

That's it. We now have a rudimentary admin area we can use to view, update, create and delete artists.

### Unit Testing Scaffolded Code

Scaffolded code is great - but let's wrap some tests around it to be sure it works.

Because the scaffolded ArtistController is just a class, we can inject our `TestDatabase.DbContext` into it and call the action methods directly:

**Rockaway.WebApp.Tests/Admin/Controllers/ArtistsControllerTests.cs**:

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp.Tests/Admin/Controllers/ArtistsControllerTests.cs %}
```

## Integration Testing Scaffolded Code

We can also use the TestFactory to run end-to-end tests on our admin controller methods, but it's considerably more involved.

Our action methods are protected by both the `[Authorize]` attribute on the controller, and the `[ValidateAntiForgeryToken]` attribute on the action itself, intended to prevent cross-site scripting attacks by ensuring they'll only accept a POST that originated from the form created by the associated GET response.

To simulate an authorized request, we need to inject a fake auth handler into the pipeline used by our test factory.

Here's the chunk of code we can use to make that happen. Note that the `FakeAuthHandler` here is a private class that's nested inside the `TestFactory` code, so there's no way anything else can inject fake authentication into our pipeline:

```csharp
class TestFactory : WebApplicationFactory<Program> {
    
	private class FakeAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions> {
        public FakeAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
            var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }
    }

    public WebApplicationFactory<Program> WithFakeAuth() => WithWebHostBuilder(builder => {
        builder.ConfigureTestServices(services => {
            services.AddAuthentication("FakeAuth")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("FakeAuth", _ => { });
        });
    });
    
    /* rest of TestFactory code goes here */
}
```

Now, we can call:

```csharp
var factory = new TestFactory();
var client = factory.WithFakeAuth().CreateClient();
```

to get a test HTTP client that'll pass our authentication checks.

Getting past the `AntiForgeryToken` is a lot more complicated, because the only way to pass this check is to do a `GET` request, extract the tokens -- which means we need to know what they're *called* -- and then including these tokens in the subsequent `POST` request.

Marinko Spasojevic covers this in a great article called [How to Include AntiForgeryToken for MVC Integration Testing](https://code-maze.com/aspnet-core-testing-anti-forgery-token/); the code in this section is adapted from Marinko's examples:

**Rockaway.WebApp.Tests/AntiForgeryTokenExtractor.cs**:

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp.Tests/AntiForgeryTokenExtractor.cs %}
```

Now we've set up helpers to handle authentication and anti-forgery tokens, we can write our test code:

**Rockaway.WebApp.Tests/WebTests/AdminTests.cs**:

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp.Tests/WebTests/AdminTests.cs %}
```

## Admin Area: Exercises

Add the ability to view, create and edit **venues**.

* This should be part of the admin area
* Only authenticated users should be able to edit venues
* Include test coverage for the create, update and delete methods















