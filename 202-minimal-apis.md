---
title: "2.2 Using Minimal APIs"
layout: module
nav_order: 0202
typora-root-url: ./
typora-copy-images-to: ./images
summary: "In this module, we'll use ASP.NET's Minimal APIs feature to add a status endpoint to our application"
previous: mwnet201
complete: mwnet202
---

In the last module, we set up continuous deployment (CD) for our project using GitHub Actions.

CD is great: run your tests, if they're green, you're good to go. But that just means you know your project worked when you deployed it... is it still working now?

We can add monitoring endpoints to various parts of our application, but I've often found it really useful to create a dedicated status endpoint that'll tell me a few vital things about the status of the application.

ASP.NET Core makes this really straightforward thanks to a feature called **minimal APIs** -- a way to map arbitrary request URLs onto snippets of code. In fact, thanks to minimal APIs, the smallest web application in .NET looks like this:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();
```

We're going to use minimal APIs to 
