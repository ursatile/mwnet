---
title: "5.2 Responsive Layouts"
layout: module
nav_order: 0502
typora-root-url: ./
typora-copy-images-to: ./images
summary: "In this module, we'll look at how to create a responsive layout for our frontend web pages."
previous: mwnet500
complete: mwnet501
---

So far, we've looked at how to customise Bootstrap's colours, fonts and other built-in elements.

Now we're going to create some elements of our own, and use SCSS to style our page layout.

First, our `_Layout` page is full of stuff that doesn't *really* need to be there.

Let's strip it back:

First, open `_Base.cshtml` and add `class="container"` to our `body` element. This means we can use Bootstrap's grid layout without having to add any more special markup.

Next, modify `_Layout.cshtml`:

```html
{% include_relative examples/502/Rockaway/Rockaway.WebApp/Pages/Shared/_Layout.cshtml %}
```

Now we're going to update our `frontend.scss` to target the new minimal page structure.

