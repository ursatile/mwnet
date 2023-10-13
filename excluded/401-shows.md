---
title: 4.1 Shows
layout: module
nav_order: 0401
summary: >-
  In this module, we'll add shows to our data model, and find out how to deal with internationalization scenarios like timezones and currency formatting.
typora-root-url: .
typora-copy-images-to: ./images
examples: examples/401/rockaway/
previous: mwnet400
complete: mwnet401
---

So far, we've modelled **artists** and **venues**. Now, we're going to model shows.

* A **show** is a ticketed event, featuring one or more **artists** performing at a **venue**
* The order of the **artists** associated with a show is significant and must be preserved.
* A **show** takes place on a specific **date**. There cannot be two **shows** at the same **venue** on the same **date**.
* A show has a **doors open** time, a **stage time** and a **curfew**
* Tickets for a **show** go on sale at a specific time
* If tickets are not yet available, the site should show the user when they will go on sale.
* Tickets can **sell out**, in which case no further tickets are available
* Tickets are withdrawn from sale **1 hour** before the doors open

Sale times, doors, stage times and curfew should always be displayed in the **local timezone of the event venue**

Ticket prices are always displayed in the **local currency of the event venue**

## Introducing NodaTime

The built-in `DateTIme` and `DateTimeOffset` classes in .NET work well, but they don't really capture the way human beings talk about dates and times.

Instead of using the built-in types, we're going to install **NodaTime**. Created by Jon Skeet, NodaTime is an open source .NET library that provides a much nicer set of abstractions over dates, times and timezones. 

> If that sounds interesting, check out the [Design philosophy and conventions](https://nodatime.org/3.1.x/userguide/design) at nodatime.org

First, install NodaTime:

```dotnetcli
dotnet add package NodaTime
```

Next, create the classes which model shows, tickets, and show line-ups.

**Rockaway.WebApp/Data/Entities/Show.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Data/Entities/Show.cs %}
```

**Rockaway.WebApp/Data/Entities/SupportSlot.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Data/Entities/SupportSlot.cs %}
```

### Seeding Test Data for Complex Entities

One of the limitations of Entity Framework is that it can only seed test data from flat entities. Our SQL data structure for a `SupportSlot` looks like this:

```
ShowVenueId: uniqueidentifier
ShowDate: date
ArtistId: uniqueidentifier
SlotNumber: int
Comments: nvarchar(max)
```

It would be lovely if EF Core would create SupportSlot records automatically from a list of Shows... but it can't.

We can use the same pattern we've used previously to set up our test data fixtures:

**Rockaway.WebApp/Data/Sample/SampleData.Shows.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Data/Sample/SampleData.Shows.cs %}
```

Instead, we need to project a set of anonymous objects which match the column names used in the database. We can do this using static extension methods:

**Rockaway.WebApp/Data/Sample/SeedDataConverters.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Data/Sample/SeedDataConverters.cs %}
```







