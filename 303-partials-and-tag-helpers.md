---
title: 3.3 Partials and Tag Helpers
layout: module
nav_order: 0303
summary: >-
  In this module, we'll learn how to use Razor features like partial views and tag helpers to create reusable elements that we can use throughout our ASPNET Core web apps. 
examples: examples/303/rockaway/
typora-root-url: .
typora-copy-images-to: ./images
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

Let's add the Slug property to our Artist entity:









In this section:

DisplayFor(Artist)

DisplayFor(Venue)

Tag Helper for country flag

