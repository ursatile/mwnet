---
title: 4.1 Shows
layout: module
nav_order: 0303
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

Next, create the classes which model shows, tickets, and show line-ups

