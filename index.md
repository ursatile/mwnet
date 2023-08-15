---
title: Home
layout: home
nav_order: 00
---



<ul id="index-nav">
{% assign contents = site.pages | where_exp:"item", "item.summary != nil" %}
{% for page in contents %}
    <li>
        <a href="{{ page.url | relative_url }}">{{ page.title }}</a>
        <p>{{ page.summary }}</p>
</li>
{% endfor %}
</ul>


**Getting started (module01.zip)**

* dotnet new web
* Look, it works!
* add a test project
* add the WebApplicationFactory
* end-to-end testing
* deployment with GitHub Actions

**module 2: introducing EF core**

**module 3: testing and logging**

**module 4: Razor Pages**

**4.1 adding a Razor page**

Add a Razor page at /artists with some very basic HTML boilerplate.

Heading, and a DisplayFor loop.

Talk through the page model behind it.

**Exercise: create a Razor page to list all the venues in the system**

4.2 ViewStart and Layouts

Make the /artists page look good. Header, footer, plug in a bit of CSS

Now look at /venues - erk. It looks like crap.

INTRODUCE:

* A layout page
* ViewStart.cshtml
* CSS - rudimentary at this stage; colours and typography

4.3 _Partials

* Set up partials for the page header and page footer.
* Plug in responsive navigation (switches to a hamburger menu on narrow displays)
  * Introduce @media queries

Working on /venues here

- Set up a DisplayTemplate for Venue
  - Name
  - Address
  - Link to buy tickets
- Set up a TagHelper for the nationality flag of the venue

EXERCISE:

* Set up a DisplayTemplate for Artist, including:
  * artist name
  * Artist description
  * Logo
  * Photo
  * Link to buy tickets

Adding shows

How do we define a show?

Displaying dates and ticket prices

Placing an order

Payment (do we integrate with Stripe here?)

Order acknowledgement

Sending a ticket

* MJML
* Papercut
* Mailtrap
* Admin endpoint to preview mail messages





















**module 5: asp.net MVC**

DAY 2:

module 7@ 

WHAT WE'RE ACTUALLY BUILDING:

* List of artists
* List of venues
* Artist page showing all forthcoming shows
* Venue page showing all forthcoming shows
* Show page listing tickets for sale
* Customer details page
* Payment simulation
* Confirmation email with tickets included









It’s 2023. .NET is a free, open-source development framework that runs on everything from Raspberry Pis to cloud data centres, server-side rendering is cool again, and despite what you might have read on LinkedIn, AI is *not* about to take away all our jobs. In this two-day, workshop, you’ll learn how to design, deliver, and test state-of-the-art web applications that harness the power of the C# language and the [ASP.NET](http://asp.net/) Core platform. Alongside familiar patterns like model/view/controller and dependency injection, the latest versions of [ASP.NET](http://asp.net/) Core introduced a streamlined hosting model, a new set of conventions for routing requests within our applications, and a completely new architecture known as “minimal APIs” for building API endpoints and microservices.
.NET also supports a thriving ecosystem of open source projects and packages. Entity Framework Core provides first-class abstractions over relational databases like MS SQL Server and PostgreSQL. Libraries like NodaTime can simplify and streamline your application logic. Projects like xUnit, Moq, and Shouldly let you test your applications at whatever level makes sense - from low-level unit testing to end-to-end integration tests that test your app’s entire HTTP pipeline. And on the front-end, it’s never been easier to incorporate technologies like SASS, CSS grids, responsive layouts, and even libraries like MJML for sending HTML emails.

**Workshop Structure**

Overview of .NET web application architecture

- .NET 6 vs .NET 7 - long term support, or latest & greatest?
- Using the dotnet CLI tool
- Creating a .NET web application
- Configuration management
- Registering services
- Routing
- Logging
- Testing your web application

Working with Relational Databases

- Lightweight data access with Dapper
- Managing data with Entity Framework Core
- Deploying database changes using EF Core Migrations
- Development on localhost using Docker

Business Logic and Domain Modelling

- Entities and data transfer objects
- Introducing abstractions
- Testing application behaviour and business logic

Locales, Times, and Timezones

- What’s wrong with System.DateTime?
- Introducing NodaTime
- Mapping conventions for custom datatypes
- Data formatting: dates, times, currencies
- Formatting data using System.Globalization

Frontend: Presentation and Validation

- Layouts, areas, partial views, and tag helpers.
- Working with Razor Pages
- Responsive layouts with CSS grid
- Hosting SASS and SCSS in .NET
- Styling forms and input validation

Deployment and Monitoring

- Cross-platform gotchas: what to watch out for when you’re developing on Windows or macOS and hosting on Linux
- Deploying to Microsoft Azure using GitHub Actions
- Application monitoring using Application Insights

**Target Audience and Prerequisites
**This workshop is aimed at developers with some experience of the C# language, the .NET platform, and some basic web development. If you understand classes, inheritance, Console. WriteLine, and you know what the <select> tag in HTML does, then you should be just fine.

**Computer Setup**
Attendees will need a computer running .NET 6 or .NET 7, Docker, and a code editor that supports .NET such as Microsoft Visual Studio 2022, JetBrains Rider, or Visual Studio Code.
