---
title: 1.4 Testing Web Apps
layout: module
nav_order: 0104
summary: >-
  In this module we'll look at various patterns and strategies for testing web applications. We'll introducing XUnit and Shouldly, we'll look at how to test features in isolation, and how to create end-to-end tests which exercise our whole web application stack.

typora-root-url: .
typora-copy-images-to: ./images
previous: mwnet103
complete: mwnet104
---

So far, we've created web endpoints using Minimal APIs, using Razor Pages, and using the Model/View/Controller pattern.

At the moment, our app works just fine, but as we add more complexity to it, there's a risk we'll break something -- and the more features we build into our application, the greater the risk that one of our changes might accidentally affect another part of the code.

To manage this, we're going to create automated tests for every feature and path through our application code. That way, if we *do* accidentally break something, we'll know about it straight away -- instead of finding out when we deploy our code and get inundated with support calls from angry customers.

Let's add some tests to our project. We're also going to create a solution file, to manage the dependencies between our web project and our test project:

```transcript
dotnet new sln
dotnet sln add Rockaway.WebApp
dotnet new xunit -o Rockaway.WebApp.Tests
dotnet sln add Rockaway.WebApp.Tests
```

Now, we can run all the tests in our solution using:

```transcript
dotnet test
```

and we should get output something like:

```
Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: < 1 ms
```

Awesome! The tests all pass - ship it!

...hang on, what are we actually testing here? Let's take a look in our new test project:

```csharp
// Rockaway.WebApp.Tests/UnitTest1.cs

namespace Rockaway.WebApp.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }
}
```

OK... so apart from the fact that it's not actually testing anything, and that `UnitTest1` is a terrible name for a class, we're off to a great start.

### Testing Controllers

One reason for the enduring popularity of the MVC pattern is that it makes it easy to test our controller logic in isolation, because our controllers are just classes.

First, we need to add a **reference**, so that our test project can see our web app. From our `Rockaway.WebApp.Tests` project folder:

```transcript
dotnet add reference ..\Rockaway.WebApp
```

Next, delete `UnitTest1.cs` and create a new file `StatusControllerTests.cs`:

**Rockaway.WebApp.Tests/StatusControllerTests.cs:**

```csharp
namespace Rockaway.WebApp.Tests;
using Rockaway.WebApp.Controllers;
using Rockaway.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

public class StatusControllerTests {
    [Fact]
    public void Status_Index_Returns_Message() {
        var c = new StatusController();
        var result = c.Index() as ViewResult;
        Assert.NotNull(result);
        var model = result.Model as SystemStatus;
        Assert.NotNull(model);
        Assert.Equal("Rockaway.WebApp is online", model.Message);
    }
}
```

This is our first example of a very common testing pattern: we instantiate a controller, call an action method on that controller, and then assert something about the action result. In this example, we're asserting that:

* The action returned a valid `ViewResult`
* The `ViewResult.Model` contained a valid `SystemStatus`
* The `SystemStatus.Message` was equal to `"Rockaway.WebApp is online"`

## Nicer Assertions with Shouldly

[https://www.nuget.org/packages/Shouldly/](https://www.nuget.org/packages/Shouldly/) is an open source .NET package which I use in all my projects, because I really like both the syntax it uses for declaring assertions, and the way it reports test failures.

First, we'll install Shouldly into `Rockaway.WebApp.Tests`:

```
dotnet add package Shouldly
```

Now, we can rewrite our assertions using Shouldly's syntax:

```csharp
namespace Rockaway.WebApp.Tests;
using Rockaway.WebApp.Controllers;
using Rockaway.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

public class StatusControllerTests {
    [Fact]
    public void Status_Index_Returns_Message() {
        var c = new StatusController();
        var result = c.Index() as ViewResult;
        result.ShouldNotBeNull();
        var model = result.Model as SystemStatus;
        model.ShouldNotBeNull();
        model.Message.ShouldBe("Rockaway.WebApp is online");
    }
}
```

## Testing Code with Dependencies

So far, so good... but really, all we're doing now is testing that strings match each other.

Our system status message also includes a timestamp; it would be good if we could test that as well - but that's where things get gnarly.

What's wrong with this code?

```csharp
[Fact]
public void Status_Index_Returns_DateTime() {
    var c = new StatusController();
    var result = c.Index() as ViewResult;
    result.ShouldNotBeNull();
    var model = result.Model as SystemStatus;
    model.ShouldNotBeNull();
    model.SystemTime.ShouldBe(DateTime.Now);
}
```

Yep - clocks tick. Every time we ask for `DateTime.Now`, we'll get a different result, so our test will always fail, because the value of `DateTime.Now` when we construct our `ViewResult` isn't going to be the same as the value when we assert the value.

`DateTime.Now` is a good example of something called **ambient context** -- a value which is just sort of hanging around in the background of our program, that we can look at from anywhere. Other examples of ambient contexts are things like `Environment.MachineName`.

Ambient contexts are easy to use, but they are almost impossible to test, because we have no control over what they're going to return, so we can't verify that they returned the correct thing.

Instead, we need to isolate information like the current date and time into a service, and then use a testable implementation of that service when it comes to testing our application logic.

### Dependency Injection in Controllers

We're going to define an interface for a clock, that exposes a single method:

**Rockaway.WebApp/Services/IClock.cs:**

```csharp
namespace Rockaway.WebApp.Services;
public interface IClock {
    DateTime Now { get; }
}
```

Now, we're going to modify our `StatusController` so that instead of using `DateTime.Now`, we provide it with a service that implements `IClock`, and the controller uses that service to get the current date and time:

**Rockaway.WebApp/Controllers/StatusController.cs:**

```csharp
{% include_relative examples/104/rockaway/Rockaway.WebApp/Controllers/StatusController.cs %}
```

Next, we add a `TestClock` to our testing project. This is a fake clock that always returns the same datetime, which we inject via the class constructor:

**Rockaway.WebApp.Tests/Services/TestClock.cs:**

```csharp
{% include_relative examples/104/rockaway/Rockaway.WebApp.Tests/Services/TestClock.cs %}
```

Now we can modify our tests to inject an instance of `TestClock` into the `StatusController`:

**Rockaway.WebApp.Tests/StatusControllerTests.cs:**

```csharp
{% include_relative examples/104/rockaway/Rockaway.WebApp.Tests/StatusControllerTests.cs %}
```

Awesome! Green tests across the board, the developers are all high-fiving each other and about to push the big red DEPLOY button... when somebody says "hang on, the website doesn't work."

What? BUT WE HAVE TESTS!

Turns out we're not testing *enough*. This kind of testing works really well for validating our controller logic, but for actually making sure our website is going to respond properly... there's a thousand ways we can break our app without causing those tests to fail.

## Testing web apps using WebApplicationFactory

As well as testing specific elements of our application logic, we're going to build in some higher-level tests which exercise the entire web application pipeline -- startup, services, request and and response.

This used to be incredibly difficult, but ASP.NET Core introduced something called the `WebApplicationFactory`, which is quite possibly the best single addition to .NET I've seen since I started using C# back in 2002. Seriously, it is amazing.

![web-application-factory-brain-meme](/images/web-application-factory-brain-meme.jpeg)



Let's use `WebApplicationFactory` to plug in an end-to-end test that'll verify that our `/status` page is actually working

First, install the package. From the `Rockaway.WebApp.Tests` folder:

```transcript
dotnet add package Microsoft.AspNetCore.Mvc.Testing
```

Next, we'll need to modify our project so that we can make our web application's code visible to our testing project.

Now we need to expose our **entry point** to the test project -- which is where it gets a bit gnarly. .NET 7 has a feature called **top level statements**:

```csharp
// top level statements:
Console.WriteLine("Look! Top level statements are awesome!");

// without top level statements:
internal class Program {
  public static void Main() {
    Console.WriteLine("Without top level statements, there's a lot more boilerplate code");
  }
}
```

Behind the scenes, the C# compiler is actually wrapping our code up in a `Program.Main()` method -- but because this all happens by magic, the resulting `Program` class is marked as `internal` -- which means that other projects, like our test project, can't see it.

There are two ways to get around this. One is to modify the `Rockaway.WebApp.csproj` file and add this chunk of XML:

```xml
<ItemGroup>
  <InternalsVisibleTo Include="Rockaway.WebApp.Tests" />
</ItemGroup>
```

The other is to add a line to our `Program.cs` which explicitly makes our `Program` class `public`:

```csharp
// Add this to the end of Program.cs

public partial class Program {}	
```

Next, we'll add a new file to our test project called `WebTests.cs`.

**Rockaway.WebApp.Tests/WebTests.cs:**

```csharp
{% include_relative examples/104/rockaway/Rockaway.WebApp.Tests/WebTests.cs %}
```

Now, when we run `dotnet test`, our test code will spin up a standalone instance of our web app, send an HTTP request to `/status`, and verify the response has a `Success` status code:

![image-20230817134717765](/images/image-20230817134717765.png)

OK, our app is broken. Good. Better to find out this way than to get angry customers shouting at our helpdesk team, right?

If we open up `/status` in a browser, we can see what's actually going on:

![image-20230817133333401](/images/image-20230817133333401.png)

We've made our `StatusController` dependent on something that implements `IClock`, but we haven't told ASP.NET Core about it -- so when that request comes in, the runtime looks at the routing map, determines that it needs to go to a `StatusController`, tries to create one, finds out that it can't make at `StatusController` without an `IClock`, realises it has no idea how to create `IClocks`, and falls over.

Let's fix it.

First, we'll create an implementation of `IClock` that uses the real system clock:

**Rockaway.WebApp/Services/SystemClock.cs:**

```csharp
{% include_relative examples/104/rockaway/Rockaway.WebApp/Services/SystemClock.cs %}
```

Next, we need to register this as a dependency -- this effectively tells the ASP.NET runtime "hey, if anybody asks you for an IClock, you give 'em one of these":

**Rockaway.WebApp/Program.cs:**

```csharp
using Rockaway.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IClock, SystemClock>();
var app = builder.Build();

app.UseRouting();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

{: .info }
We're registering the `SystemClock` here using the `Services.AddSingleton` method. This tells ASP.NET to create one single instance of the `SystemClock` -- the "singleton" instance -- and then pass this same instance to any request that asks for an `IClock`. We'll look more at component lifecycles in a later module.

Now, when we re-run our tests:

![image-20230817135005051](/images/image-20230817135005051.png)

Sweet.

## Testing Dependencies with WebApplicationFactory

Being able to test our web code end-to-end like this is pretty cool, but as it stands right now, we've just moved the problem. Earlier we saw how we can test code which uses `DateTime.Now` by isolating it behind a service, and then injecting a test version of that  service. Now we've taken a few steps back, we're testing our entire web stack from end to end, including the application startup and services in `Program.cs` -- which means we're back to having a real system clock in our test code.

Say that we want to verify that our status page is returning the system date in ISO8601 format, including the time zone offset:`2023‐08‐17T06:06:48−07:00`

We've already got a test that verifies that the `StatusController` retrieves the current datetime from an `IClock` service and passes this into the model, but this doesn't test anything about how that datetime is actually formatted when it's rendered onto the status page.

What we need to do is somehow inject a test clock into the actual website code, send a request to `/status`, get the response back as an HTML page, and then inspect that response to verify that it contains the correct date format.

First, we need to replace the real clock with a test clock. We can do this by inheriting from `WebApplicationFactory<T>` and overriding the `ConfigureWebHost` method:

**Rockaway.WebApp.Tests/TestFactory.cs:**

```csharp
{% include_relative examples/104/rockaway/Rockaway.WebApp.Tests/TestFactory.cs %}
```

Now, we can create test methods which use that new `TestFactory` class:

```csharp
[Fact]
public async Task GET_Status_Includes_ISO3601_DateTime() {
    var testDateTime = new DateTime(2023,4,5,6,7,8);
    var clock = new TestClock(testDateTime);
    var factory = new TestFactory(clock);
    var client = factory.CreateClient();
    var response = await client.GetAsync("/status");
    var html = await response.Content.ReadAsStringAsync();
    html.ShouldContain(testDateTime.ToString("o"));
}
```

### Parsing HTML with AngleSharp

A good test tells you WHEN your application doesn't work; a great test tells you WHY your application doesn't work.

If the test above passes, all we know is that the request produced a web page, and the web page contained the string `2023-04-05T06:07:08+01:00` - and if it fails, the error we get isn't terribly useful:

![image-20230817144641851](/images/image-20230817144641851.png)

It would be more useful if we could dig into the HTML markup of the page, extract the element we're interested in, make sure it exists, and make sure it contains the correct value.

There's a great .NET open source library called [https://github.com/AngleSharp/AngleSharp](https://github.com/AngleSharp/AngleSharp), which provides "the ability to parse angle bracket based hyper-texts like HTML, SVG, and MathML."

Let's install AngleSharp into our test project:

```
dotnet add package AngleSharp
```

Then we'll modify our view and wrap the system time in a `<span>` tag with a specific `id`, which we can use to target this element in our test:

**Rockaway.WebApp/Views/Status/Index.cshtml:**

```html
@model Rockaway.WebApp.Models.SystemStatus

<!DOCTYPE html>
<html>
  <head>
    <title>Home</title>
  </head>
  <body>
    <h1>@Model.Message</h1>
    <p>
      The time is
      <span id="system-time">@Model.SystemTime?.ToString("O")</span>
    </p>
  </body>
</html>

```

Then, we're going to modify our test to parse and validate a specific HTML tag:

```csharp
protected IBrowsingContext browsingContext => BrowsingContext.New(Configuration.Default);

[Fact]
public async Task GET_Status_Includes_ISO3601_DateTime_Element() {
    var testDateTime = new DateTime(2023,4,5,6,7,8);
    var clock = new TestClock(testDateTime);
    var factory = new TestFactory(clock);
    var client = factory.CreateClient();
    var response = await client.GetAsync("/status");
    var html = await response.Content.ReadAsStringAsync();
    var dom = await browsingContext.OpenAsync(req => req.Content(html));
    var element = dom.QuerySelector("#system-time");
    element.ShouldNotBeNull();
    element.InnerHtml.ShouldBe(testDateTime.ToString("O"));
}
```

...and we're done. We can spin up a full end-to-end web stack, replace specific services with test versions, send a request, parse the response, and validate specific elements of the resulting HTML.

## Testing Web Applications: Exercises

In the previous section, we added `Environment.MachineName` and the assembly last modified date to our status page.

Add test coverage for these two features. You'll need to create interfaces which expose the required properties, test implementations of these interfaces to use in your unit and integration tests, and real implementations that return live data from the system hosting your app.















