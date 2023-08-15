---
title: 3. Testing and Logging
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

In other words, SQLite is a tiny database engine that doesn't use a server: it runs in the same process as our application code, which makes it perfect for running isolated tests that still need to connect to some sort of database.

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

namespace Rockaway.WebApp.Tests;

class TestDatabase {

	public static async Task<RockawayDbContext> Create(string? dbName = null) {
		dbName ??= Guid.NewGuid().ToString();
		var dbContext = Connect(dbName);
		await dbContext.Database.EnsureCreatedAsync();
		return dbContext;
	}

	public static RockawayDbContext Connect(string dbName) {
		var connectionString = $"Data Source={dbName};Mode=Memory;Cache=Shared";
		var sqliteConnection = new SqliteConnection(connectionString);
		sqliteConnection.Open();
		var options = new DbContextOptionsBuilder<RockawayDbContext>()
			.UseSqlite(sqliteConnection)
			.Options;
		return new(options);
	}
}
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

## Configuring EF Core Logging

EF Core takes away a lot of the work involved in using relational databases, but that doesn't mean it always gets it right. When working with tools like EF Core, it's important to understand what the tools are actually doing for us, so that we can see right away if one of our methods begins to generate abnormal or problematic database queries.

### Logging to Console.WriteLine

The simplest way to see what's happening internally is to configure EF Core to log absolutely everything to the console:

```csharp
var options = new DbContextOptionsBuilder<RockawayDbContext>()
	.UseSqlite(sqliteConnection)
	.LogTo(Console.WriteLine)
	.Options;
```

The `LogTo` method takes an `Action<string>`, so we can pass in any method which accepts a string input -- such as the built-in `Console.WriteLine` method.

This produces a *vast* amount of information, because by default it includes all debug and diagnostic information generated by EF Core:

```
dbug: 2023-08-01 14:45:32.960 CoreEventId.ContextInitialized[10403] (Microsoft.EntityFrameworkCore.Infrastructure) 
      Entity Framework Core 7.0.9 initialized 'RockawayDbContext' using provider 'Microsoft.EntityFrameworkCore.Sqlite:7.0.9' with options: None
dbug: 2023-08-01 14:45:32.979 RelationalEventId.CommandCreating[20103] (Microsoft.EntityFrameworkCore.Database.Command) 
      Creating DbCommand for 'ExecuteScalar'.
dbug: 2023-08-01 14:45:32.984 RelationalEventId.CommandCreated[20104] (Microsoft.EntityFrameworkCore.Database.Command) 
      Created DbCommand for 'ExecuteScalar' (9ms).
dbug: 2023-08-01 14:45:32.985 RelationalEventId.CommandInitialized[20106] (Microsoft.EntityFrameworkCore.Database.Command) 
      Initialized DbCommand for 'ExecuteScalar' (12ms).
dbug: 2023-08-01 14:45:32.991 RelationalEventId.CommandExecuting[20100] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executing DbCommand [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 2023-08-01 14:45:33.008 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (18ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
dbug: 2023-08-01 14:45:33.074 RelationalEventId.TransactionStarting[20209] (Microsoft.EntityFrameworkCore.Database.Transaction) 
      Beginning transaction with isolation level 'Unspecified'.
dbug: 2023-08-01 14:45:33.079 RelationalEventId.TransactionStarted[20200] (Microsoft.EntityFrameworkCore.Database.Transaction) 
      Began transaction with isolation level 'Serializable'.
dbug: 2023-08-01 14:45:33.082 RelationalEventId.CommandCreating[20103] (Microsoft.EntityFrameworkCore.Database.Command) 
      Creating DbCommand for 'ExecuteNonQuery'.
dbug: 2023-08-01 14:45:33.082 RelationalEventId.CommandCreated[20104] (Microsoft.EntityFrameworkCore.Database.Command) 
      Created DbCommand for 'ExecuteNonQuery' (0ms).
dbug: 2023-08-01 14:45:33.082 RelationalEventId.CommandInitialized[20106] (Microsoft.EntityFrameworkCore.Database.Command) 
      Initialized DbCommand for 'ExecuteNonQuery' (0ms).
dbug: 2023-08-01 14:45:33.083 RelationalEventId.CommandExecuting[20100] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executing DbCommand [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Artists" (
          "Id" TEXT NOT NULL CONSTRAINT "PK_Artists" PRIMARY KEY,
          "Name" TEXT NOT NULL,
          "Description" TEXT NOT NULL
      );
info: 2023-08-01 14:45:33.084 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Artists" (
          "Id" TEXT NOT NULL CONSTRAINT "PK_Artists" PRIMARY KEY,
          "Name" TEXT NOT NULL,
          "Description" TEXT NOT NULL
      );
dbug: 2023-08-01 14:45:33.088 RelationalEventId.TransactionCommitting[20210] (Microsoft.EntityFrameworkCore.Database.Transaction) 
      Committing transaction.
dbug: 2023-08-01 14:45:33.090 RelationalEventId.TransactionCommitted[20202] (Microsoft.EntityFrameworkCore.Database.Transaction) 
      Committed transaction.
dbug: 2023-08-01 14:45:33.092 RelationalEventId.TransactionDisposed[20204] (Microsoft.EntityFrameworkCore.Database.Transaction) 
      Disposing transaction.
dbug: 2023-08-01 14:45:33.131 CoreEventId.ValueGenerated[10808] (Microsoft.EntityFrameworkCore.ChangeTracking) 
      'RockawayDbContext' generated a value for the property 'Id.Artist'. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see key values.
dbug: 2023-08-01 14:45:33.173 CoreEventId.StartedTracking[10806] (Microsoft.EntityFrameworkCore.ChangeTracking) 
      Context 'RockawayDbContext' started tracking 'Artist' entity. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see key values.
dbug: 2023-08-01 14:45:33.177 CoreEventId.SaveChangesStarting[10004] (Microsoft.EntityFrameworkCore.Update) 
      SaveChanges starting for 'RockawayDbContext'.
dbug: 2023-08-01 14:45:33.179 CoreEventId.DetectChangesStarting[10800] (Microsoft.EntityFrameworkCore.ChangeTracking) 
      DetectChanges starting for 'RockawayDbContext'.
dbug: 2023-08-01 14:45:33.185 CoreEventId.DetectChangesCompleted[10801] (Microsoft.EntityFrameworkCore.ChangeTracking) 
      DetectChanges completed for 'RockawayDbContext'.
dbug: 2023-08-01 14:45:33.229 RelationalEventId.CommandCreating[20103] (Microsoft.EntityFrameworkCore.Database.Command) 
      Creating DbCommand for 'ExecuteReader'.
dbug: 2023-08-01 14:45:33.229 RelationalEventId.CommandCreated[20104] (Microsoft.EntityFrameworkCore.Database.Command) 
      Created DbCommand for 'ExecuteReader' (0ms).
dbug: 2023-08-01 14:45:33.230 RelationalEventId.CommandInitialized[20106] (Microsoft.EntityFrameworkCore.Database.Command) 
      Initialized DbCommand for 'ExecuteReader' (1ms).
dbug: 2023-08-01 14:45:33.232 RelationalEventId.CommandExecuting[20100] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executing DbCommand [Parameters=[@p0='?' (DbType = Guid), @p1='?', @p2='?' (Size = 11)], CommandType='Text', CommandTimeout='30']
      INSERT INTO "Artists" ("Id", "Description", "Name")
      VALUES (@p0, @p1, @p2);
info: 2023-08-01 14:45:33.236 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (5ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?', @p2='?' (Size = 11)], CommandType='Text', CommandTimeout='30']
      INSERT INTO "Artists" ("Id", "Description", "Name")
      VALUES (@p0, @p1, @p2);
dbug: 2023-08-01 14:45:33.240 RelationalEventId.DataReaderClosing[20301] (Microsoft.EntityFrameworkCore.Database.Command) 
      Closing data reader to 'main' on server ''.
dbug: 2023-08-01 14:45:33.243 RelationalEventId.DataReaderDisposing[20300] (Microsoft.EntityFrameworkCore.Database.Command) 
      A data reader for 'main' on server '' is being disposed after spending 4ms reading results.
dbug: 2023-08-01 14:45:33.247 CoreEventId.StateChanged[10807] (Microsoft.EntityFrameworkCore.ChangeTracking) 
      An entity of type 'Artist' tracked by 'RockawayDbContext' changed state from 'Added' to 'Unchanged'. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see key values.
dbug: 2023-08-01 14:45:33.248 CoreEventId.SaveChangesCompleted[10005] (Microsoft.EntityFrameworkCore.Update) 
      SaveChanges completed for 'RockawayDbContext' with 1 entities written to the database.
dbug: 2023-08-01 14:45:33.567 CoreEventId.QueryCompilationStarting[10111] (Microsoft.EntityFrameworkCore.Query) 
      Compiling query expression: 
      'DbSet<Artist>()'
dbug: 2023-08-01 14:45:33.680 CoreEventId.QueryExecutionPlanned[10107] (Microsoft.EntityFrameworkCore.Query) 
      Generated query execution expression: 
      'queryContext => new SingleQueryingEnumerable<Artist>(
          (RelationalQueryContext)queryContext, 
          RelationalCommandCache.QueryExpression(
              Projection Mapping:
                  EmptyProjectionMember -> Dictionary<IProperty, int> { [Property: Artist.Id (Guid) Required PK AfterSave:Throw ValueGenerated.OnAdd, 0], [Property: Artist.Description (string) Required, 1], [Property: Artist.Name (string) Required MaxLength(100), 2] }
              SELECT a.Id, a.Description, a.Name
              FROM Artists AS a), 
          null, 
          Func<QueryContext, DbDataReader, ResultContext, SingleQueryResultCoordinator, Artist>, 
          Rockaway.WebApp.Data.RockawayDbContext, 
          False, 
          False, 
          True
      )'
dbug: 2023-08-01 14:45:33.694 RelationalEventId.CommandCreating[20103] (Microsoft.EntityFrameworkCore.Database.Command) 
      Creating DbCommand for 'ExecuteReader'.
dbug: 2023-08-01 14:45:33.694 RelationalEventId.CommandCreated[20104] (Microsoft.EntityFrameworkCore.Database.Command) 
      Created DbCommand for 'ExecuteReader' (0ms).
dbug: 2023-08-01 14:45:33.694 RelationalEventId.CommandInitialized[20106] (Microsoft.EntityFrameworkCore.Database.Command) 
      Initialized DbCommand for 'ExecuteReader' (0ms).
dbug: 2023-08-01 14:45:33.694 RelationalEventId.CommandExecuting[20100] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executing DbCommand [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "a"."Id", "a"."Description", "a"."Name"
      FROM "Artists" AS "a"
info: 2023-08-01 14:45:33.695 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "a"."Id", "a"."Description", "a"."Name"
      FROM "Artists" AS "a"
dbug: 2023-08-01 14:45:33.709 RelationalEventId.DataReaderClosing[20301] (Microsoft.EntityFrameworkCore.Database.Command) 
      Closing data reader to 'main' on server ''.
dbug: 2023-08-01 14:45:33.710 RelationalEventId.DataReaderDisposing[20300] (Microsoft.EntityFrameworkCore.Database.Command) 
      A data reader for 'main' on server '' is being disposed after spending 14ms reading results.
```

We don't need all that. All we really need is the **info** messages, so we can pass this in as a **log level**:

```csharp
var options = new DbContextOptionsBuilder<RockawayDbContext>()
	.UseSqlite(sqliteConnection)
	.LogTo(Console.WriteLine, LogLevel.Information)
	.Options;
```

That filters the output down to a much more manageable level:

```
info: 2023-08-01 14:55:12.947 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (8ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT COUNT(*) FROM "sqlite_master" WHERE "type" = 'table' AND "rootpage" IS NOT NULL;
info: 2023-08-01 14:55:13.022 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE "Artists" (
          "Id" TEXT NOT NULL CONSTRAINT "PK_Artists" PRIMARY KEY,
          "Name" TEXT NOT NULL,
          "Description" TEXT NOT NULL
      );
info: 2023-08-01 14:55:13.167 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (4ms) [Parameters=[@p0='?' (DbType = Guid), @p1='?', @p2='?' (Size = 11)], CommandType='Text', CommandTimeout='30']
      INSERT INTO "Artists" ("Id", "Description", "Name")
      VALUES (@p0, @p1, @p2);
info: 2023-08-01 14:55:13.535 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "a"."Id", "a"."Description", "a"."Name"
      FROM "Artists" AS "a"
```

{: .note } You'll see there are actually four statements here, because our test code is creating and populating the database before running the test itself. We'll come back to this later.

### Using Microsoft.Extensions.Logging in EF Core

`Console.WriteLine` is great for local development, but for CI pipelines and production deployments, we can't rely on console output ending up anywhere useful, so we need to use a more flexible approach. **Microsoft.Extensions.Logging** gives us a way to connect our application's logging output to a variety of providers, and is fully supported by EF Core.

Here's the code for a test database that uses `Microsoft.Extensions.Logging` to connect to a console logger:

```csharp
{% include_relative examples/module03/Rockaway.WebApp.Tests/TestDatabase.cs %}
```







