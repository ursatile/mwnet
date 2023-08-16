---
title: 4. Introducing Razor Pages
layout: home
nav_order: 04
summary: >-
  We've got a database, tests, and logging. In this module, we'll add Razor Pages support to our application and create our first data-driven web page.
---

So far, we've wired up a database, added logging and testing code, and generally built a solid foundation for starting to develop the pages and features that will make up our application.

In this module, we're going to add support for Razor Pages to our web app.

## Minimal APIs vs Razor Pages vs Model-View-Controller

We've already seen the **Minimal APIs** pattern, that allows us to map a route such as `/artists` directly onto a function in our application code. In the next section, we'll add support for the **model/view/controller (MVC)** pattern, and see how this popular pattern is supported in ASP.NET Core.

Each of these patterns has their own strengths and weaknesses.

* Minimal APIs is fast, lightweight, and ideal for building endpoints which return JSON or XML responses. 
* Razor Pages works well for simple, standalone web pages, such as lists and contact forms
* MVC works well for more complex scenarios, such as online purchasing, user registration, and other features requiring roich interactions across multiple forms and screens.

## Adding Razor Pages to our application

First, we need to add support for the Razor Pages feature to our app. In `Program.cs`:

1. Add `builder.Services.AddRazorPages();` right before we call `builder.Build()`
2. Add `app.MapRazorPages()` before we call `app.Run();`

We're also going to remove the `/artists` endpoint - you'll see why in a moment.

```csharp
// Rockaway.WebApp/Program.cs

{% include_relative examples/module04/Rockaway.WebApp/Program.cs %}
```

This should cause one of our existing tests to fail, because `GET /artists` now returns an error.

Let's fix it. First, add a new folder to `Rockaway.WebApp` called `Pages`, and then inside `Pages`, create two new files:.

First, `Artists.cshtml`. This file contains the Razor code which actually renders our page:

```html
@* Rockaway.WebApp/Pages/Artists.cshtml *@

{% include_relative examples/module04/Rockaway.WebApp/Pages/Artists.cshtml %}
```

Next, create a file alongside it called `Artists.cshtml.cs`. This contains our **page model** -- the C# code which handles database connections and initialises any data used by the page.

```csharp
// Rockaway.WebApp/Pages/Artists.cshtml.cs

@page
@model Rockaway.WebApp.Pages.ArtistsPageModel

@{
	ViewBag.Title = "Artists";
}
<h1>Artists</h1>
@foreach (var artist in Model.Artists) {
	@Html.DisplayFor(_ => artist)
}
```













