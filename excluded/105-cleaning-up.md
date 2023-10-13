---
title: 1.5 Cleaning Up
layout: module
nav_order: 0105
summary: >-
  Before we move on to the next part of the workshop, let's take a moment to clean up our code, apply some formatting conventions, and generally make sure everything's nicely structured and organised before we go any further.
typora-root-url: .
typora-copy-images-to: ./images
previous: mwnet104
complete: mwnet105
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

## Formatting code with .editorconfig and dotnet format

Finally, we're going to apply some formatting rules to our project, and set up a `.editorconfig` file to enforce these rules.

{: .note }
EditorConfig is a way to maintain consistent coding styles for multiple developers working on the same project across various editors and IDEs. You can read more about it at [editorconfig.org](https://editorconfig.org)

You can find the  `.editorconfig` file we'll use here: [.editorconfig](examples/105/rockaway/.editorconfig)

Add this file to your solution folder alongside your `Rockaway.sln` file. 

{: .note }
**For Windows Users:** To create an `.editorconfig` file within Windows Explorer, you need to create a file named `.editorconfig.` (note the trailing dot), which Windows Explorer will automatically rename to `.editorconfig` for you.

After adding the `.editorconfig` file, reformat all the files in your project to match the project's new formatting settings.

Then run:

```transcript
dotnet format
```

That will reformat all the `.cs` files in the solution to conform to the code style specified in `.editorconfig`

{: .highlight }
The `.editorconfig` used in this  workshop uses tabs for indentation, not spaces. I used to prefer spaces for indentation. Then I read Adam Tuttle's article  "[Tabs vs Spaces: It's an Accessibility Issue](https://adamtuttle.codes/blog/2021/tabs-vs-spaces-its-an-accessibility-issue/)", and that completely changed my mind. I can use tabs. No big deal. But there are developers out there for whom tabs vs spaces is a Big Deal. Developers with visual impairments who use an extra-large font size, who set their tab width to 1 character. Developers using Braille displays, for whom a tab only occupies a single Braille cell. So now I use tabs wherever I can.

If you're using Visual Studio, Resharper, or Jetbrains Rider, you'll get access to a whole lot more formatting tools and features than you get with the `dotnet format` command line. The good news is that all these applications respect settings defined in your `.editorconfig` file, so you can maintain consistent code formatting per-project across multiple projects, teams and code styles.









