---
title: 1.3 Shouldly and AngleSharp
layout: module
nav_order: 0103
typora-root-url: ./
typora-copy-images-to: ./images
summary: "In this module we cover some more advanced test patterns you can use with the WebApplicationFactory, and two of my favourite NuGet packages - Shouldly and AngleSharp."
previous: mwnet102
complete: mwnet103
---

Let's install some more NuGet packages. **Shouldly** is a library for doing fluent assertions with .NET, and **AngleSharp** gives us the ability to query HTML documents from C#.

```
dotnet add Rockaway.WebApp.Tests package Shouldly
dotnet add Rockaway.WebApp.Tests package AngleSharp
```

Now, we're going to add a test which verifies that our homepage has the proper `<title>` element. We'll use the `WebApplicationFactory` to retrieve the page's HTML, and then use `AngleSharp` to extract the right element and `Shouldly` to assert that it has the right value.

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

We can also use xUnit's `[InlineData]` attribute to apply the same test to multiple pages. Say we want to confirm that multiple pages on our site have the correct `<title>` tags:

```csharp
[Theory]
[InlineData("/", "Rockaway")]
[InlineData("/privacy", "Privacy Policy - Rockaway")]
[InlineData("/contact", "Contact Us - Rockaway")]
public async Task Page_Has_Correct_Title(string url, string title) {
    var browsingContext = BrowsingContext.New(Configuration.Default);
    var factory = new WebApplicationFactory<Program>();
    var client = factory.CreateClient();
    var html = await client.GetStringAsync(url);
    var dom = await browsingContext.OpenAsync(req => req.Content(html));
    var titleElement = dom.QuerySelector("title");
    titleElement.ShouldNotBeNull();
    titleElement.InnerHtml.ShouldBe(title);
}
```



## Exercises

#### Add a "Contact Us" page

1. Create a "Contact Us" page as part of the Rockaway web application.
2. Create a test that verifies that the Contact Us page URL returns a successful response
3. Create a test that verifies that the Contact Us page has the correct page title

#### Extra Credit

How would you verify that the Contact Us page includes the correct email address and telephone number?



