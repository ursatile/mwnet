---
title: 2.2 Our First Entity
layout: module
nav_order: 0202
summary: >-
  In this module we'll create the Artist entity, used to model the performers and musicians in our ticketing system, and see how to use EF Migrations to apply changes to our SQL database.
examples: examples/202/rockaway/
typora-root-url: .
typora-copy-images-to: ./images
---

Creating the Artist class

The first business object we're going to add to our data model is `Artist`.

First, create a class representing an artist. For now, all we need is a **name** and a **description**

**Rockaway.WebApp/Data/Entities/Artist.cs:**

```csharp
namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public string Name { get; set; } = String.Empty;
	public string Description { get; set; } = String.Empty;
}
```

We're defaulting the `Name` and `Description` properties to `String.Empty`. Strings in .NET are `null` until they're assigned a value, but .NET 7 doesn't like `nulls`; we need to either mark these properties as nullable by making them `string?` instead of `string`, or use a `!` annotation to suppress the compiler warning -- or just give them a default value so we know they'll never be null.

{: .note }
Why does this matter? Imagine we wrote some search code that ran `artist.Name.Contains("van halen")` -- if `Name` was null, that code would throw a `NullReferenceException`. Making `Name` non-nullable means this won't happen.

Next, we're going to tell our data context that our database has Artists in it. 

**Rockaway.WebApp/Data/RockawayDbContext.cs:**

```csharp
namespace Rockaway.WebApp.Data;

public class RockawayDbContext : DbContext {
	// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
	// if we want to use Asp.NET to configure our database connection and provider.
	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }

	public DbSet<Artist> Artists { get; set; }

	// A helper property which we'll use to test the connection and return the server version.
	public string ServerVersion => Database.SqlQueryRaw<string>("SELECT @@VERSION as Value").First();
}
```

{: .note }

Don't worry about `Artists` ending up as null here. The `DbContext` base constructor guarantees this will never happen, and EF Core 7 suppresses the compiler warning about nullable reference types. Older versions of EF *would* cause compiler warnings about `Artists` being null, which could be suppressed either by assigning `Artist => Set<Artist>()`, or by assigning `Artists { get; set; } = null!`, but in EF Core 7+ we can just go back to plain old `{ get; set; }` for these properties.

Now, let's modify our Artists Razor page to return a list of artists from the database:

**Rockaway.WebApp/Pages/Artists.cshtml:**

```html
{% include_relative {{ page.examples }}Rockaway.WebApp/Pages/Artists.cshtml %}
```

**Rockaway.WebApp/Pages/Artists.cshtml.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Pages/Artists.cshtml.cs %}
```

Browse to that page and...

![image-20230818132735624](/images/image-20230818132735624.png)

Welcome, friends to something I call **error message driven development**.

The error here looks pretty obvious: 

> InvalidOperationException: The entity type 'Artist' requires a primary key to be defined.

So let's define one. EF Core will help us out a bit here: if we define a property called `Id`, it'll automatically pick that up and use it as the primary key.

```csharp
namespace Rockaway.WebApp.Data.Entities;

public class Artist {
    public Guid Id { get; set; }
	public string Name { get; set; } = String.Empty;
	public string Description { get; set; } = String.Empty;
}
```

Reload the page, and... 

![image-20230818133001051](/images/image-20230818133001051.png)

Cool - a *different* error message!

> SqlException: Invalid object name 'Artists'.

Yep. There isn't an object in our **database** called `Artists` -- we've created the **class**, but haven't created a corresponding **table**

## Working with EF Migrations

EF Core includes a tool called **migrations**, which lets us generate and apply modification scripts to keep our database schema in sync with our application code.

It's a mature, powerful tool. **It's also one of the easiest ways to shoot yourself in the foot if you don't use it carefully.**

We're going to install the EF migrations tooling:

```
dotnet tool install --global dotnet-ef
```

Then we need to install the `Microsoft.EntityFrameworkCore.Design` package in our web app:

```
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Check the tooling is installed by running `dotnet ef` and looking for the unicorn 🦄:

```
                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\

Entity Framework Core .NET Command-line Tools 7.0.9

Usage: dotnet ef [options] [command]
```

Next, we're going to create a migration that will perform the initial setup of our database:

```
dotnet ef migrations add InitialCreate
```

> The EF Migrations tooling generates C# code that won't conform to the style rules of our project, so it's always worth running `dotnet format` straight after generating a migration.

Here's what gets generated:

```csharp
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class InitialCreate : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Artists",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Artists", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Artists");
		}
	}
}
```

There's one thing there which stands out as something we might regret later: the `Name` and `Description` columns are both `nvarchar(max)`. `nvarchar` means they'll be created as Unicode string columns, which is fine, but `(max)` means they're unlimited.

Unlimited is fine for articles, blog posts, emails... but we're going to be using these fields to build screens and pages which have to work across multiple devices, so let's take a moment and think about this. The best way to find sensible lengths is to look for real-world outliers. The `Artist` table in our model is to store artists -- bands, musicians, orchestras -- and searching the web for long band names gives us:

* `The Silver Mount Zion Memorial Orchestra and Tra-La-La Band` *(60 letters)*
* `Tim and Sam's Tim and the Sam Band with Tim and Sam` *(52 characters)*
* `…And You Will Know Us By The Trail Of The Dead` *(48 letters)*
* `The Presidents of the United States of America` *(46 letters)*
* `Richard Cheese & Lounge Against The Machine` *(43 letters)*

So 100 Unicode characters -- `nvarchar(100)` -- looks like a pretty good choice for our artist name column.

We'll limit `Description` to 500 characters. That's long enough to tell our users a bit about the band they're looking at, but short enough that it's not going to give us all kinds of headaches when it comes to design and page layout.

Let's remove that migration:

```
dotnet ef migrations remove
```

There's two different ways we can control the column lengths that get generated by the migration tool.

One is to include data annotations in our entity class. The `System.ComponentModel.DataAnnotations` namespace defines many useful annotations which are respected by EF Core, including `MaxLength`:

```csharp
using System.ComponentModel.DataAnnotations;

namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;
	[MaxLength(500)]
	public string Description { get; set; } = String.Empty;
}
```

The other way is to override the `OnModelBuilding` method in our `DbContext`

```csharp
public class RockawayDbContext : DbContext {
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<Artist>(entity => {
			entity.Property(e => e.Name).HasMaxLength(100);
			entity.Property(e => e.Description).HasMaxLength(500);
		});
	}
}
```

This is known as the **fluent API**. In case of a conflict, the fluent API takes precedence; configuration is applied in the order that the methods are called, and the last one wins.

We're going to use data annotations, because EF Core isn't the only part of the .NET runtime that will make use of them; these same annotations can be used for things like client-side validation, so our decision to limit artist name to 100 characters will be reflected throughout our codebase, not just in our database schema.

After adding those data annotations, we can regenerate our migration script:

```
dotnet ef migrations add InitialCreate
```

and then apply it to our database:

```
dotnet ef database update
```

Now, when we browse to `/artists`, we'll get an almost-blank page - which is good; it means everything worked.

## Populating the Database

Populating our database with an initial set of data is known as **data seeding**, and once we've set up our empty database, there are several different ways to populate it with data:

1. Insert it directly, by running `INSERT` statements in a tool like SQL Server Management Studio
2. Run SQL scripts as part of a migration
3. Use EF Core's static data feature
4. Create a web interface for managing data, and use this.

## Using EF Core's HasData() feature

We're going to use the `HasData` feature of EF Core to populate our database with a set of sample artists.

First, we need some sample data. I'm a big fan of creating realistic sample datasets for applications; they make it much easier to build meaningful tests, to generate mockups and screenshots, and visualise user journeys through the finished application.

> Sample data is also a great place to catch edge cases. Use Unicode strings, empty strings, missing fields... anything that your users might throw at you once your app is running in production.

Here's an example of how I create and manage test data in web applications. You can see the full sample data set at [SampleData.cs]({{ page.examples }}Rockaway.WebApp/Data/Sample/SampleData.cs)

```csharp
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Rockaway.WebApp.Data.Sample;

public partial class SampleData {
	private static Guid TestGuid(int seed, char pad) => new(seed.ToString().PadLeft(32, pad));
	public static class Artists {
		public static Artist AlterColumn = new() {
			Id = TestGuid(1, 'a'),
			Name = "Alter Column",
			Description = "Alter Column are South Africa's hottest math rock export. Founded in Cape Town in 2021, their debut album \"Drop Table Mountain\" was nominated for four Grammy awards."
		};

		public static Artist Ærbårn = new() {
			Id = TestGuid(27, 'a'),
			Name = "Ærbårn",
			Description = "Inspired by their Australian namesakes, Ærbårn are Scandinavia's #1 party rock band. Thundering drums, huge guitar riffs and enough energy to light up the Arctic Circle, their shows have had amazing reviews all over the world"
		};

		public static IEnumerable<Artist> AllArtists = new[] {
			AlterColumn, BinarySearch, Ærbårn
		};
	}
}     
```

Now we can call this from the `OnModelCreating` override in our `RockawayDbContext`:

**Rockaway.WebApp/Data/RockawayDbContext.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Data/RockawayDbContext.cs %}
```

This doesn't actually *do* anything yet, because to actually populate the SQL database with artists, we'll need to create and run another migration:

```
dotnet ef migrations add PopulateSampleArtistData
dotnet format
dotnet ef database update
```

Now, browse to `/artists`:

![image-20230818154439111](/images/image-20230818154439111.png)





