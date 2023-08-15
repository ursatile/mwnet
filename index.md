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
**Monday 09:00-10:00**

20 minutes of talk:

* Principles of web architecture
* Inputs & outputs

20 minutes of demo:

* Minimal APIs
* Requests and responses

20 minutes of exercise:

* String reverser
* Current UTC date and time
* Calculator

**BREAK**

Monday 10:15-11:15

20 minutes of talk:

* Razor pages and the page authoring model
* Introducing MVC
* Key differences

20 minutes of demo:

* Add a Razor Pages endpoint
* Add a model, view & controller
* Add a layout page

Exercise:

* Work out how to share layouts between pages and views

**BREAK**

Monday 11:30-12:30

Logging, Formatting, Testing

Testing

* Start with trivial MVC endpoint test

Testing with the WebApplicationFactory

Test a method that returns the current date and time

Deployment - let's get a site running on Azure

**LUNCH**

13:30-14:30 Introducing Entity Framework

* **Scenario: a list of venues**
* Create a DbContext
* Create an entity (**Venue**)
* create an empty DB (docker)
* Introduce migrations
* Inspect migration
* Apply migration
* Populate with some data

DATA EXPLAINED:

* Static data
* Test data
* Sample data
* Live data

EXERCISE:

* **A list of artists**
* Create the Artist entity (data supplied)
* Add it to the database
* Migration

**14:30-14:45 break**

14:45-15:45 Using SQLite for local development

* Get SQLite set up locally
* Show how to use it for end-to-end tests
* Show how to use it for local dev mode (including connection persistence)
  * Memory mode
  * On-disk storage mode

15:45-16:00 break

16:00-17:00 Frontend presentation and styling

* SASS/SCSS
* TagHelpers
* Partials
* Display templates

DAY 2

**09:00-10:00 Admin and editing data**

* Admin dashboard
* Security - *open question: how do we want to secure this?*
  * Real security for production
  * Fake security for localhost development

* Edit screen for artists
* Client-side validation
* EXERCISE: Edit screen for venues

**10:15-11:15 Shows and tickets**

* Data modelling: what does a show look like?
* Ticket price - how to store it?
* Showtime: how do we fit that in?
* Displaying ticket prices

**11:30-12:30 The user checkout journey**

* Choosing tickets
* Validation and running totals
* Client-side currency formatting
* Showing your basket

*12:30-13:30 LUNCH*

**13:30-14:30 Integrating with an external payment system (do I have time to make a fake one of these?)**

*14:30-14:45 BREAK*

**14:45-15:45 Sending a ticket email**

* Introducing Mailkit
* Razor + MJML
* Papercut
* Mailtrap.IO
* HTML vs Text Mode
* Attachments

15:45-16:00 break

**16:00-17:00 Monitoring and Application Insights**
