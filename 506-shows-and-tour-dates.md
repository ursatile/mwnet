---
title: "5.6 Shows and Tour Dates"
layout: module
nav_order: 0506
typora-root-url: ./
typora-copy-images-to: ./images
summary: "Let's send our artists on tour - time to add shows to our application."
previous: mwnet505
complete: mwnet506
examples: examples/506/Rockaway/
---

So far, our application's data model is pretty simple - artists and venues, and a bunch of string properties describing each one.

Here's what we're going to add:

1. Tour dates. A way to advertise the fact that a particular band will be performing at a certain venue, on a certain date.
2. Tickets. What kinds of ticket are available for each show, how much do they cost, and how many of that type of ticket are available?
3. Checkout process. A customer can add tickets to an order and submit the order.
4. Email: when a customer submits an order, we'll send them an email confirming it.

### Shows

A show is defined by an **artist** performing at a **venue** on a specific **date**. The featured artist is known as the **headliner**.

* The same venue cannot host two different shows on the same date, so we can use the combination of `(venue, date)` as a unique identifier for a show. This is known as a **composite key**.
* A show might feature additional artists (known as **support acts**). Each support act is assigned a numbered **support slot**. By convention, these are in reverse running order: if there are three support acts, the band who are "fourth on the bill" will play first, then the band who are in slot #3, then the band in slot #2, and then the headliner. 

A show has a **doors time** (what time does the venue start admitting ticket holders), a **stage time** (what time does the show start), and a **curfew** (what time does the venue close).

A show has one or more **ticket types**: each ticket type has a **name**, a **price** and a **sales limit**. If the sales limit is `null`, it means there's no limit.

### Dates, Times and DateTimes, Oh My!

Let's start with the easy part: venues don't move, and so if we know where a venue *is*, we can work out what time zone it's in, and that's not going to change. Probably.

If a show's on March 25th, in Los Angeles, and the doors open at 7pm, it's already March 26th for most of the rest of the world.

One common strategy for dealing with this kind of scenario is to insist that all date/time data is stored in the database as UTC, and the convert it to/from local time whenever you're displaying it. This can work well, but it does introduce an element of risk -- if somebody forgets to implement the conversion when adding a new UI feature, we could end up with shows on the wrong day.

What we really want is a **local date**; a way of saying "look, this event has a location, and it happens when the date **in that location** is March 25th".

.NET's DateTime classes have never provided great support for this kind of scenario: a .NET `DateTime` has a `Kind` property, which can be `Local`, `Utc` or `Unspecified`, but it's still way too easy to get the conversions wrong.

Instead, we're going to install a library called **NodaTime**, created by Jon Skeet and originally inspired by the JodaTime libraries for Java.

> Noda Time is an alternative date and time API for .NET. It helps you to think about your data more clearly, and express operations on that data more precisely.
>
> - [Project web site](http://nodatime.org/) - for documentation, installation, downloads etc
> - [Group/mailing list](https://groups.google.com/group/noda-time) - for discussion of potential features
> - [Project source and issue site](https://github.com/nodatime/nodatime)
> - [Stack Overflow tag](http://stackoverflow.com/questions/tagged/nodatime) - for specific "How do I do X?" questions

```
dotnet add package NodaTime
```

NodaTime gives us access to a whole bunch of new classes, including one called `LocalDate`, which works perfectly for our scenario.

Here's the code for a `Show`, including the `LocalDate` storing the show date:

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Data/Entities/Show.cs %}
```



















