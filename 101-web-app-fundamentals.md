---
title: 1.1 Web App Fundamentals
layout: module
nav_order: 0101
summary: >-
  In this module we'll review the underlying principles of web applications - because, deep down, all we're doing is slinging fancy strings across the network. We'll see how HTTP and HTML are the fundamental building blocks of web apps, and we'll introduce .NET Minimal APIs and use them to build our first web app.
typora-copy-images-to: ./images
---

## Client, Servers, Requests and Responses

The web is the most successful distributed system ever built. Billions of servers running dozens of different operating systems, languages, databases, from hobby servers running on low-cost hardware like Raspberry Pis to serverless functions running in the cloud, all forming part of a giant global network of interconnected documents and services.

Behind every single one of those sites and services is the same fundamental architecture: clients, servers, requests and responses.

## Fundamental Principles of Web Applications

### Servers don't do anything until they receive a request

Think of a web server like a bartender, and the client as a customer ordering drinks. When a customer orders a gin & tonic -- the *request* -- the bartender springs into action and makes them a gin & tonic -- the *response*.

What if there are no customers? Well, there's odd bits of housekeeping and maintenance to do, sure, but one of the key principles of web application architecture is that if there are no requests coming in, your server shouldn't be doing anything. 

And if there are too many customers? The bartender can't keep up; customers have to wait longer for their drinks. And some of them get fed up of waiting -- we say their *request timed out*. Maybe they order again - "hey, where's my gin & tonic?" -- or maybe they go to another bar.

Either way, timeouts are bad.

### Structure of an HTTP Request

[graphic here]

### Structure of an HTTP Response

[graphic here]

### Getting started with ASP.NET Core Web Apps

We're going to use ASP.NET Core to explore what we can do using HTTP requests and responses.

First, make sure you've got the right version of .NET installed. We're using .NET 7 for these examples, so run:

```
D:\Projects> dotnet --version
7.0.201
```

As long as it starts with a 7, you should be good to go. (If it starts with an 8, I'll assume you know what you're doing. 😉)

## Creating the MyWebApp application

Next, we're going to use the `dotnet` command and one of the built-in project templates to create our new web application.


Running `dotnet new --list` will show you a list of all the available project templates. There's a *lot* of them in .NET 7, covering everything from Windows Forms applications to iOS class libraries.

Create a new folder called `workshop`, then inside your new folder, run:

```
D:\workshop> dotnet new web -o MyWebApp
D:\workshop> cd MyWebApp
D:\workshop\MyWebApp> dotnet run
```

Somewhere in the output, you'll see a line like:

```
Now listening on: http://localhost:5165
```

so open that URL in a browser -- you might find you can Ctrl-Click on the URL in the terminal window; if not, copy & paste it -- and you should see something like this:

{: .note }
The URL that your web app runs on uses a port number that's generated at random when you create the app; if you want to change it, it's defined in the `launchSettings.json` file which you'll find in `MyWebApp/Properties/`

Let's take a look at the code that makes it work. Open up `MyWebApp/Program.cs`: 

```csharp
// MyWebApp/Program.cs

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

That's an entire web application, including the server, in four lines of C# code -- not bad.

 The `dotnet new web` template we've used here uses **minimal APIs** to map incoming HTTP requests onto code - in this case, the line:

```csharp
app.MapGet("/", () => "Hello World!");
```
maps any HTTP GET request to `/` (the root URL) to an inline function which takes no arguments, and returns the string `"Hello World!"`.

### URL Parameters

You can specify placeholders in the request URL, and ASP.NET Core will automatically bind these parameters to method arguments, as in this example:

```csharp
app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");
```

URL parameters don't have to be strings; here's an endpoint that will multiply two integers:

```csharp
app.MapGet("/multiply/{x}/{y}", (int x, int y) => x * y);
```

{: .question }
What happens if you call `/multiply/foo/bar` ?

### QueryString parameters

ASP.NET Core will also bind query string parameters to method arguments:

```csharp
app.MapGet("/multiply", (int x, int y) => x * y);
```

If you call `multiply` you'll get an error -- but if you call `/multiply?x=5&y=7`, you'll get the right answer.

### Returning File Content

You can return the contents of a file using the `Results` object:

```csharp
app.MapGet("/form" , () => Results.File("form.html", "text/html"));
```

If you specify a relative file path like `"form.html"`, it's resolved relative to something called the [WebRootFileProvider](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostenvironment.webrootfileprovider?view=aspnetcore-7.0#microsoft-aspnetcore-hosting-iwebhostenvironment-webrootfileprovider) -- by default, this means it'll look in a folder called `wwwroot` at the root of your application. The [`Results`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results) class used here provides static helper methods for many common  scenarios such as returning file contents, JSON documents, controlling the HTTP response code, and more. 

### Hosting Static Files

You can also host static file content directly by calling `app.UseStaticFiles(); ` before you call `app.Run();` -- again, by default this will serve content from a `wwwroot` folder at the root of your project; call the [`builder.UseWebRoot()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.hostingabstractionswebhostbuilderextensions.usewebroot?view=aspnetcore-7.0) method before calling `builder.Build()` if you want to override this.

### Form parameters

Here's the HTML code for a simple calculator form:

```html
<!DOCTYPE html>
<html>
    <head>
        <title>The World's Dumbest Calculator</title>
    </head>
    <body>
        <form action="/multiply" method="POST">
            <label for="x-input">X:</label>
            <input type="number" id="x-input" name="x" placeholder="0" required /><br />
            <label for="y-input">Y:</label>
            <input type="number" id="y-input" name="y" placeholder="0" required /><br />
            <input type="submit" value="Multiply" />
        </form>
    </body>
</html>
```

This form will submit using an http `POST` request to `/result` -- but the parameters `x`, `y` and `op` will be passed in the request body as `multipart/form-data`, and ASP.NET Core Minimal APIs **doesn't support automatically binding these parameters**

Instead, what we can do is to pass the `HttpContext` into our handler method, and read the values ourselves:

``` csharp
app.MapPost("/result" , (HttpContext ctx) => {
    Int32.TryParse(ctx.Request.Form["x"], out int x);
    Int32.TryParse(ctx.Request.Form["y"], out int y);
    return $"Result: {x * y}";
});
```

### Gotcha: Minimal APIs and hot reload

`dotnet` has a built-in file watcher: you can also run your app using `dotnet watch run`, which will watch for any changes in your application files and rebuild the application. However,  .NET 6 introduced a feature called **hot reload**, and the combination of minimal APIs, hot reload and `dotnet watch run` doesn't always do what you expect. The problem is that if you modify your `Program.cs` , the runtime will reload the affected methods -- but because the code in `Program.cs` only runs once, when your application is first started, reloading it doesn't actually have any effect.

To get the expected behaviour, specify `--no-hot-reload` when running your application:

```
dotnet watch --no-hot-reload --project MyWebApp run
```

This will rebuild **and restart** your application whenever you change a file, including changes to `Program.cs`, and then refreshing the browser will show you the new state of the application code.

### Working with Minimal APIs: Exercises

#### String Reverser

Create a GET endpoint that will return a string passed into it as a URL parameter:

`GET /reverse/hello-world`  should return `dlrow-olleh`

#### Day of the Week

Create a GET endpoint that will accept a date in `yyyy-MM-dd` format and tell you what day of the week it was:

`GET /dotw/1978-01-01` should return `Sunday`

`GET /dotw/2038-01-19` should return `Tuesday`

#### Calculator

Here's the source code for a very simple calculator input form:

```html
<!DOCTYPE html>
<html>
    <head>
        <title>The World's Dumbest Calculator</title>
    </head>
    <body>
        <form action="/calculate" method="POST">
            <label for="x-input">X:</label>
            <input type="number" id="x-input" name="x" value="0" required /><br />
            <label for="y-input">Y:</label>
            <input type="number" id="y-input" name="y" value="0" required /><br />
            <input type="submit" name="op" value="+" />
            <input type="submit" name="op" value="-" />
            <input type="submit" name="op" value="*" />
            <input type="submit" name="op" value="/" />
        </form>
    </body>
</html>
```

This will submit an HTTP POST to `/calculate`.

1. Add an `app.MapPost` method to your app that will perform the calculation and return the result as an integer.
2. Modify your method to return the result as an HTML page containing the parameters and the result. **Use only minimal APIs for this part. The point is to see first-hand what's involved in building HTML pages by manipulating strings.**
3. Modify your method to return a graceful error if you try to divide by zero.

## Minimal APIs: References and Further Reading

* **Introduction to ASP.NET Core Minimal APIs** by Khalid Abuhakmeh:

  [https://blog.jetbrains.com/dotnet/2023/04/25/introduction-to-asp-net-core-minimal-apis/](https://blog.jetbrains.com/dotnet/2023/04/25/introduction-to-asp-net-core-minimal-apis/)

* **Tutorial: Create a minimal API with ASP.NET Core** at learn.microsoft.com:
  [https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api)



