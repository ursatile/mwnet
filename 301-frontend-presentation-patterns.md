---
title: 3.1 Frontend Presentation Patterns
layout: module
nav_order: 0301
summary: >-
  In this module, we'll learn about Razor patterns and features we can use to create responsive, reusable frontends, including partials, display templates and tag helpers. We'll also learn how to incorporate SCSS into our web application so we can use "syntactically awesome stylesheets" in our ASP.NET Core web apps.
examples: examples/301/rockaway/
typora-root-url: .
typora-copy-images-to: ./images
---

So far, we've focused on backend development patterns -- HTTP endpoints, routing, databases, testing.

Time to shift our focus and look at some frontend presentation patterns.

## Layouts and Partials

The pages we've created in the previous sections don't look like much. We're going to fix that, but first we're going to impose some structure and consistency on the pages used in our application.

Our app is using a combination of Razor Pages and MVC Views; ideally, we'd like to reuse as much of our frontend code as possible so we don't need to maintain two copies of everything. Let's start by setting up a common page layout, header and footer.

First, create two new files at the root of our web application:

**Rockaway.WebApp/_ViewStart.cshtml:**

```html
{% include_relative {{page.examples}}Rockaway.WebApp/_ViewStart.cshtml %}
```

**Rockaway.WebApp/_ViewImports.cshtml:**

```html
{% include_relative {{page.examples}}Rockaway.WebApp/_ViewStart.cshtml %}
```

> Most .NET project templates put `_ViewStart` in the `~/Views/` or the `~/Pages/` folder, but the Razor engine will actually find them anywhere in the project's folder hierarchy, so by putting them in the project root, both the MVC Views engine and the Razor Pages engine will find the **same file**.

Take a quick look at the error messages we get now:

Browsing to `/artists` (which is a Razor Pages page) gives us:

```
InvalidOperationException: The layout view '_Layout' could not be located. The following locations were searched:
/Pages/_Layout.cshtml
/Pages/Shared/_Layout.cshtml
/Views/Shared/_Layout.cshtml

Microsoft.AspNetCore.Mvc.Razor.RazorView.GetLayoutPage(ViewContext context, string executingFilePath, string layoutPath)
```

whereas browsing to `/status`, which is an MVC view, gives:

```
InvalidOperationException: The layout view '_Layout' could not be located. The following locations were searched:
/Views/Status/_Layout.cshtml
/Views/Shared/_Layout.cshtml
/Pages/Shared/_Layout.cshtml

Microsoft.AspNetCore.Mvc.Razor.RazorView.GetLayoutPage(ViewContext context, string executingFilePath, string layoutPath)
```

Looks like we can put our layout in either `/Views/Shared/_Layout.cshtml` OR in `/Pages/Shared/_Layout.cshtml`, and both engines will pick it up.

> If we wanted to put it somewhere completely different, like `/Common/Web/Things/Layouts/_Layout.cshtml`, we could do that too, but we'd need to specify the entire relative path name in `_ViewStart.cshtml`

I've decided we'll use `~/Views/Shared` for all our reusable bits of Razor code, so let's create a layout.

## A Word about Semantic Markup

Lots of modern websites are built using frameworks which produce markup like this:

![image-20230818194242944](/images/image-20230818194242944.png)

I don't like this. I think it's lazy and inelegant.

In this workshop, we're going to use semantic markup. Our page header will be a `<header>` tag, our footer will be a `<footer>`. The main bit of the page will be `<main>`. We'll organise content using `<section>`, we'll use `<nav>` and `<label>` and `<fieldset>` and all the other weird and wonderful tags that browsers know about.

![image-20230818195111041](/images/image-20230818195111041.png)

<figcaption>The Periodic Table of HTML elements: <a href="https://madebymike.github.io/html5-periodic-table/">madebymike.github.io/html5-periodic-table</a></figcaption>















 
