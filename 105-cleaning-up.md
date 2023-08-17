---
title: 1.5 Cleaning Up
layout: module
nav_order: 0105
summary: >-
  Before we move on to the next part of the workshop, let's take a moment to clean up our code, apply some formatting conventions, and generally make sure everything's nicely structured and organised before we go any further.
typora-root-url: .
typora-copy-images-to: ./images
---

Here's a quick recap of what we've accomplished so far.

* We've learned how to build HTTP handlers and endpoints using minimal APIs
* We've learned how to use Razor Pages to build interactive HTML pages
* We've learned about the model/view/controller pattern, and how to break our application down into discrete, testable components
* We've learned how to test controller actions using xUnit
* We've learned how to run end-to-end web tests using the WebApplicationFactory
* We've learned how to parse and validate HTML using AngleSharp

Before we move on, we're going to look at a few tools and features we can use to clean up our code, eliminate redundancies, and generally reduce the amount of stuff we need to look at it figure out what a given file in our codebase is doing.

## Global Usings

C# version 10 added support for a new language directive called **global usings**.

Previously, we had to explicitly import every namespace we wanted to use, at the top of every file. Now, we can specify a `global using` and make a specified namespace available to every file in our project.

You can put a `global using` anywhere in your project, but the convention is to put them all in one file. Most of the .NET templates name this file `Usings.cs`. JetBrains Resharper prefers to call it `GlobalUsings.cs`. I like Resharper, so we're gonna call it `GlobalUsings.cs`.

Global using directives are useful in many situations, but they're particularly useful in test projects where each test class imports a *lot* of namespaces -- framework code, project code, and various tools and libraries we're using as part of our test fixtures.

**Rockaway.WebApp.Tests/GlobalUsings.cs:**

```csharp
{% include_relative examples/105/rockaway/Rockaway.WebApp.Tests/GlobalUsings.cs %}
```











