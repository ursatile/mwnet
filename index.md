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

**module 2: Routing, pages, areas and MVC** 

**module 3: services and dependency injection**

LUNCH

module 4: adding a database.

* Add an Artist class
* Add an `IEnumerable<Artist>` to the data interface
* Make a fake one
* Inject it.

EXERCISE: do the same with Venue



module 5: data model

module 6: frontend styling and SCSS



DAY 2:

module 7@ 



