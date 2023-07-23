---
title: 3. Testing EF Core
layout: module
nav_order: 03
summary: >-
  Testing code which relies on an external database has always been a challenge. In this module, we'll learn about using SQLite's in-memory database to create fast, lightweight tests that still use EF Core and relational database operations.
---

In the last module, we set up a SQL Server database, added EF Core to our application, and wired up two endpoints that use data from our database.

We even put a couple of tests around them to prove they worked.

There's only one problem: the tests only work if the database is running. If we stop Docker: boom, failing tests. If we're running a CI build on Github Actions or TeamCity: boom, failing tests.

That's bad. Really bad. **Tests should only fail if the code is broken.** Tests that fail when there's actually nothing wrong mean we start ignoring them... and then when we break something for real, we don't notice.

In this module, we'll see how to configure our application so we can run end-to-end tests of our web application, using a lightweight, standalone database that runs as part of our test code.

## Introducing SQLite

> SQLite is a C-language library that implements a [small](https://sqlite.org/footprint.html), [fast](https://sqlite.org/fasterthanfs.html), [self-contained](https://sqlite.org/selfcontained.html), [high-reliability](https://sqlite.org/hirely.html), [full-featured](https://sqlite.org/fullsql.html), SQL database engine. SQLite is the [most used](https://sqlite.org/mostdeployed.html) database engine in the world. SQLite is built into all mobile phones and most computers and comes bundled inside countless other applications that people use every day.
>
> *from [https://sqlite.org/](https://sqlite.org/)*

In other words, SQLite is a tiny database engine that doesn't use a server: it runs in the same process as our application code.

Sweet.

Let's see what we need to do to get our test code using SQLite instead of SQL Server.

First, we're going to install the SQLite provider for Entity Framework Core - **remember to install this into the Rockaway.WebApp.Tests project, not the main WebApp project**

```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

While we're here, we're also going to install Shouldly, a fluent assertions library for .NET. Shouldly lets us write assertions like `artist.Name.ShouldBe("Van Halen")`, and if it isn't, we get helpful error messages like: `Failure: artist.Name should be "Van Halen" but was "Spice Girls" at WebTests.cs line 45`

```powershell
dotnet add package Shouldly
```

Next, we're going to create a `TestDatabase` which provides helper methods for creating and connecting to a SQLite version of our application database.

```csharp
// Rockaway.WebApp.Tests/TestDatabase.cs

{% include_relative examples/module03/Rockaway.WebApp.Tests/TestDatabase.cs %}
```

{: .note }
There may be scenarios where we want a test to explicitly create two separate connections to the same database, so this class exposes separate  `Create` and `Connect` methods. In most cases, we'll call `Create` to get a new, empty database, populate it as we need to, and inject it into the application code that we're testing.

We're also going to create a `TestFactory`, which will inherit from the `WebApplicationFactory<T>` we saw earlier, and give us a way to replace the "real" database with our SQLite test database:

```csharp
// Rockaway.WebApp.Tests/TestFactory.cs

{% include_relative examples/module03/Rockaway.WebApp.Tests/TestFactory.cs %}
```

Now, we can create test code which looks like this:

```csharp
[Fact]
public async Task GET_Artists_Returns_Correct_Data() {
	// Arrange
	var factory = new TestFactory();
	var artist = new Artist { Name = "Test Artist" }; 
	factory.DbContext.Artists.Add(artist);
	await factory.DbContext.SaveChangesAsync();

    // Act
	var client = factory.CreateClient();
	var response = await client.GetAsync("/artists");
	
    // Assert
	var json = await response.Content.ReadAsStringAsync();
	var data = JsonConvert.DeserializeObject<List<Artist>>(json);
	var result = data.Single();
	result.Name.ShouldBe("Test Artist");
}
```

That runs entirely in-process. No external dependencies, no servers; every test gets its own completely standalone, isolated database (unless we explicitly specify a database name).

Best of all: on my dev machine, that entire test runs in around 700 milliseconds -- create a new database, connect to it, create the data schema, create the test data, spin up the web application pipeline, send the request, parse the response, and clean everything up afterwards.

That's AWESOME.

Really.

