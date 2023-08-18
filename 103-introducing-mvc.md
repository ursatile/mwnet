---
title: 1.3 ASP.NET MVC
layout: module
nav_order: 0103
summary: >-
  In this module, we'll introduce the model/view/controller (MVC) pattern, and learn how ASP.NET Core implements support for applications which use this pattern.

typora-root-url: ./
typora-copy-images-to: ./images
---

In the last module, we looked at Razor Pages, and learned about the page-based routing model.

Page-based routing has been around almost as long as the web; I remember building web apps in Perl in the late 1990s which used this pattern, and it's been used by PHP, classic ASP, ASP.NET WebForms, and dozens of web frameworks.

For more sophisticated web applications, though, we need a more flexible approach. We want to be able to decouple our request handling, application logic, and presentation into separate components which we can then use to deliver richer use cases and user journeys.

[TODO: diagram here]

ASP.NET has supported the model/view/controller pattern since 2008 -- in fact, ASP.NET MVC (as it was then known) was the first official open source .NET project ever shipped by Microsoft.

MVC breaks our application into three distinct sets of components.

**Controllers** handle incoming requests, extract parameter values, handle authentication, caching... basically, all the HTTP-level stuff.

**Models** are your domain classes. This is *your* business logic -- your customers, your products, your policies and price lists and Frequent Flyer miles and whatever else it is your application actually does.

**Views** make it all look nice when it ends up in a web browser.

### Adding MVC to our ASP.NET App

ASP.NET Core ships with a new project template that'll wire up a full MVC app for you:

```
dotnet new mvc -o MyMvcApp
```

Which is fine... but as with the Razor Pages template in the last section, that gives us a whole raft of stuff we don't need yet.

So instead, we're going to wire MVC support into our application by hand so we can see what's happening at every step of the process.

We'll add `builder.Services.AddControllersWithViews()` and `app.MapControllerRoute()` to our `Program`:

**Rockaway.WebApp/Program.cs:**

```csharp
{% include_relative examples/103/rockaway/Rockaway.WebApp/Program.cs %}
```

Next, we're going to add an MVC endpoint that returns a system status page.

We'll create a **model** representing the status of our system:

**Rockaway.WebApp/Models/SystemStatus.cs:**

```
{% include_relative examples/103/rockaway/Rockaway.WebApp/Models/SystemStatus.cs %}
```

and a **view** that'll format that status message as an HTML web page:

**Rockaway.WebApp/Views/Status/Index.cshtml:**

```html
{% include_relative examples/103/rockaway/Rockaway.WebApp/Views/Status/Index.cshtml %}
```

Finally, we'll plug in a **controller** with an **action** method which will populate a new instance of `SystemStatus` and pass it to the `View` method:

**Rockaway.WebApp/Controllers/StatusController.cs:**

```csharp
{% include_relative examples/103/rockaway/Rockaway.WebApp/Controllers/StatusController.cs %}
```

Now, browsing to `/status` will show us a system status page:

![image-20230817120650987](/images/image-20230817120650987.png)





### Working with Model/View/Controller: Exercises:

* 







