---
title: 2. Connecting to a Database
layout: home
nav_order: 02
summary: >-
  In this module, we'll connect our app to a relational database using Entity Framework (EF) Core, and create our first data-driven HTTP endpoint.
---

In the last module, we created a .NET web application with a single "hello world" endpoint, and added some testing around this to verify that it worked.

In this module, we're going to start adding some application data to our web app.

The business scenario we're modelling here is tickets to rock concerts, so the first thing we're going to add here is a list of artists -- bands and musicians who are going to be performing at the shows we're selling tickets for.

The starting code for this module is in module01.zip.

## Adding the Artist class

1. Create a new folder in `Rockaway.WebApp` called `Data`
2. Create a new folder inside `Data` called `Entities`
3. Create a new class inside `Entities` called `Artist.cs`:

```csharp
// Rockaway.WebApp/Data/Entities/Artist.cs

{% include_relative examples/module02/Rockaway.WebApp/Data/Entities/Artist.cs %}
```

This is a relatively simple class, but there are a handful of things to notice here:

First, we're using a GUID (Globally Unique Identifier) as an identifier.

We need a way to uniquely identify two artists, even if they have exactly the same name. (This happens more than you might think: [Classic Rock has a whole article about it.](https://www.loudersound.com/features/different-bands-same-name-nirvana-iron-maiden-slayer))

| Integer keys                                                 | GUID keys                                                    |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| <i class="fa-regular fa-thumbs-up"></i> Small (4 bytes)      | <i class="fa-regular fa-thumbs-down"></i> Large (16 bytes)   |
| <i class="fa-regular fa-thumbs-up"></i> Easy to remember *("hey, it's customer 27!")* | <i class="fa-regular fa-thumbs-up"></i> Hard to remember *("OK, the ID is... you got a pen? AB7C9DFA-0278-BBF0-A45F-A78B9C89D78F... got that?")* |
| <i class="fa-regular fa-thumbs-down"></i> Easy to guess *("oooh... I wonder who customer 28 is!")* | <i class="fa-regular fa-thumbs-up"></i> Basically impossible to guess. |
| <i class="fa-regular fa-thumbs-up"></i> Good for monolithic systems where IDs are assigned by a central authority. | <i class="fa-regular fa-thumbs-up"></i> Good for distributed systems where we might need to merge IDs that were generated offline |
| <i class="fa-regular fa-thumbs-up"></i> Auto-incrementing means new rows are added to the *end* of the table, so inserts are fast | <i class="fa-regular fa-thumbs-down"></i>New IDs don't increment (unless we use a special generator) so inserts might mean repaginating data. |

We're going to use GUIDs for our IDs. Mainly because I like GUIDs; there are some legitimate criticisms of using GUIDs as keys, but honestly, they're mostly academic unless we're dealing with terabytes of data, or specific scenarios where you need to insert a lot of records in a hurry. For the vast majority of day-to-day applications, GUIDs work just fine.

Second, we're defaulting the `Name` and `Description` properties to the empty string. Strings in .NET are `null` until they're assigned a value, but .NET 7 doesn't like `nulls`; we need to either mark these properties as nullable by making them `string?` instead of `string`, or use a `!` annotation to suppress the compiler warning -- or just give them a default value so we know they'll never be null.

{: .note }
Imagine we wrote some search code that ran `artist.Name.Contains("van halen")` - if `Name` was null, that code would throw a `NullReferenceException`. Making `Name` non-nullable means this won't happen.

## Installing Entity Framework Core

Next, we're going to install EF Core, and set up a `DbContext` that we can use to connect our application code to a database.



