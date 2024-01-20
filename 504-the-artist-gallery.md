---
title: "5.4 The Artist Gallery"
layout: module
nav_order: 0504
typora-root-url: ./
typora-copy-images-to: ./images
summary: "Let's add some photographs, and turn our artists page into something that might inspire customers to actually buy a ticket."
previous: mwnet503
complete: mwnet504
---

In this module, we're going to overhaul the design of the `/artists` page, and then add a new artist information page at `/artists/{slug}` that will show us details of a specific artist.

One approach is to do this using Razor Pages:

* Modify the existing `/Pages/Artists.cshtml` page to include hyperlinks for each artist
* Add a new page at `/Pages/Artist.cshtml` that includes a route parameter for the artist `slug` property, used to look up the artist in the database.

