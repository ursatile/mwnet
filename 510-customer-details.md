---
title: "5.10 Customer Details"
layout: module
nav_order: 10510
typora-root-url: ./
typora-copy-images-to: ./images
summary: "In this module, we'll build the second part of the customer checkout process, confirming a customer's order and asking them to enter their name and email address."
previous: mwnet509
complete: mwnet510
examples: examples/510/Rockaway
---

In the last module, we created a simple ticket picker, and when it was submitted, added a `TicketOrder` to the database.

In this module, we'll capture the customer's name and email address, and ask them to accept our payment terms.

> Integrating with a real payment system is outside the scope of this workshop, so for now, we just ask the customer to promise that they'll pay on the door. Chill. It'll be fine.

We're going to create a new controller, `CheckoutController`.

`GET /checkout/confirm/{id}` will display the order contents and ask the customer to fill in their contact details.

`POST /checkout/confirm` will confirm their order (and, in the next module, send them an email)

```csharp
// Rockaway.WebApp/Controllers/CheckoutController.cs

{% include_relative {{ page.examples }}/Rockaway.WebApp/Controllers/CheckoutController.cs %}
```

