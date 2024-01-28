---
title: "7.1 Razor Components"
layout: module
nav_order: 10701
typora-root-url: ./
typora-copy-images-to: ./images
summary: "Razor Components are a way to build reusable, interactive controls and components without writing JavaScript. In this module, we'll build an interactive TicketPicker using Razor Components."
previous: mwnet602
complete: mwnet701
examples: examples/701/Rockaway
---

Let's upgrade our ticket picker.

At the moment, it looks like this:

![image-20240128022308557](/images/image-20240128022308557.png)

Wouldn't it be cool if, instead of typing numbers in the boxes, you could use little plus/minus buttons to add and remove tickets? And get realtime feedback on the total price of your order - in the right currency, properly formatted?

What if you could do all that without writing any JavaScript?

Let's meet Razor Components: a way to combine HTML, Razor and C# code to create interactive components without writing any JavaScript.

