---
title: Introduction
layout: home
nav_order: 0000
typora-copy-images-to: ./images
summary: "Introduction - why .NET, why C#, choosing a .NET version. The scenario: selling tickets for rock concerts."
---

## Introduction

In this workshop, we're going to use .NET 7, C# and ASP.NET Core to build a web application for selling tickets to rock concerts.

I chose this scenario for several reasons. First, it's relatively simple to understand. Bands go on tour, they play shows in various different venues, maybe there's a support band or two on the same bill, but the business rules are fairly consistent from one show to the next.

Second, dealing with stage times and ticket prices at venues in different countries means we can't ignore real-world concerns like time zones and currency localization.

Third: I like rock bands.🤘🏼

![shutterstock_676097989_1080p](images/shutterstock_676097989_1080p.jpg)

## Choosing a .NET Version

In the beginning, there was just .NET -- I've been using .NET since 2002, when I went to a launch event at Microsoft in the UK and got a beta release of "Visual Studio .NET" on DVD.

In 2014, Microsoft announced .NET Core -- an open-source, cross-platform version of .NET -- and so the older, Windows-only version of .NET became known as .NET Framework.

.NET Core shipped versions 1.0, 2.0 and 3.1, alongside .NET Framework 4.6, 4.7 and 4.8.

Since the release of .NET 5 in 2020, there's been a release of .NET every November.

Even-numbered versions are "long term support" releases, with support and updates guaranteed for at least three years. Odd-numbered versions are short term support, with updates for 18 months.

At the time of writing, the latest LTS release is .NET 6, which will be supported until the end of 2024, and the latest STS release is .NET 7, released in November 2022. 

.NET 8 is due out in November 2023, and will be supported until November 2026. 

However, very few breaking changes are anticipated between .NET 7 and .NET 8, so the examples in this workshop are all based on .NET 7, and should migrate to .NET 8 without requiring any code changes.

> There will be a handful of new features in .NET 8 which replace some of the patterns and techniques covered in this workshop; I'll highlight these as we go along, but the methods shown here will still work just fine; .NET 8 just offers a slightly easier way to achieve the same thing.

