---
title: "4.2 The Admin Area"
layout: module
nav_order: 0402
typora-root-url: ./
typora-copy-images-to: ./images
summary: "In this module, we'll move our CRUD controllers into a dedicated area called Admin, and secure this area so it's only available to authenticated users."
previous: mwnet401
complete: mwnet402
---

## Areas in ASP.NET Core

> Areas are **an ASP.NET feature used to organize related functionality into a group as a separate namespace (for routing) and folder structure (for views)**. Using areas creates a hierarchy for the purpose of routing by adding another route parameter, area , to controller and action or a Razor Page page .
>
>  	- [https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/areas?view=aspnetcore-7.0](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/areas?view=aspnetcore-7.0)

The controllers we've set up so far support Create, Read, Update, Delete operations - often known as "CRUD controllers". 

We don't really want random internet strangers to be creating and deleting records in our database, so let's move these controllers into a dedicated area which we can restrict to authenticated users only.

We'll use the `dotnet` command line tools to create our new area. Inside your `Rockaway.WebApp` project folder, run:

```
dotnet aspnet-codegenerator area Admin
```

All this generator actually does is create a bunch of empty folders... but hey, saves us having to create them ourselves, right?

Next, we need to move our controllers and views into the new area:

```
/Controllers
	/ArtistsController.cs	--> /Areas/Admin/Controllers/ArtistsController.cs
	/VenuesController.cs	--> /Areas/Admin/Controllers/VenuesControllers.cs
/Views
	/Artists/* 				--> move everything to /Areas/Admin/Views/Artists
	/Venues/* 				--> move everything to /Areas/Admin/Views/Venues
```

*(If you do this in an IDE, it'll probably fix the namespaces for you; if not, you'll need to fix them yourself)*

Add the `[Area("admin")]` attribute to both of our controllers:

```csharp
[Area("admin")]
public class VenuesController : Controller {
  //...
}
```

Add a new route to `Program.cs` which uses our admin area, and use the `RequireAuthorization` method to restrict this area to users who are already signed in.

```csharp
app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
).RequireAuthorization();
```

> For this workshop, we're not restricting users - *any* authenticated user can access our admin area. For a production app, you'd need to either restrict the ability to create new users, or user a more restrictive policy so that only selected users could access admin features.

Let's move the standard layouts with all the Bootstrap & scaffolding stuff into their own area, so we can keep all the advantages of rapid development for admin area code, but have total control of our customer-facing app.

### Move the login partial

Move `_LoginPartial.cshtml` to `/Areas/Admin/Views/Shared`

Remove the reference to LoginPartial from `/Views/Shared/_Layout.cshtml`

While we're there, let's add an admin link to the page footer:

### Create the admin layout

`Rockaway.WebApp/Areas/Admin/Views/Shared/_AdminLayout.cshtml`

```html
{% include_relative examples/402/Rockaway/Rockaway.WebApp/Areas/Admin/Views/Shared/_AdminLayout.cshtml %}
```

### Set up _ViewStarts for the Admin and Identity areas

Create `/Areas/Admin/_ViewStart.cshtml` :

```
@{
	Layout = "_AdminLayout";
}
```

Create `/Areas/Identity/Pages/_ViewStart.cshtml` (which overrides the "invisible" page that's part of the UI assembly):

```csharp
@{
    Layout = "/Areas/Admin/Views/Shared/_AdminLayout.cshtml";
}
```

### Create the admin home page:

`Areas/Admin/Pages/Index.cshtml`

```html
{% include_relative examples/402/Rockaway/Rockaway.WebApp/Areas/Admin/Pages/Index.cshtml %}
```

Lock down the home page (and any other Razor Pages under `/admin/`)

```csharp
builder.Services.AddRazorPages(options => options.Conventions.AuthorizeAreaFolder("admin","/"));
```

And we're done:

* We've moved our CRUD controllers into an area under `/admin`
* We've locked down this area so that controllers and pages inside this area are only accessible to authenticated users.
  * Note how we've had to authorise controllers and pages separately; the URLs look the same but we're dealing with two completely separate parts of the ASP.NET routing system so we need to authorise each of them separately.
* We've created a separate layout for our admin area and our frontend area
