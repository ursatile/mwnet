### Mocking DbContext

First, we can use a **mocking library** to create a lightweight replica of `RockawayDbContext`, known as a **mock**, which can stand in for the real one in our test code.

We're going to use the Moq library for this. 

{: .note }

Moq has recently been the subject of some online controversy, after the authors added a package called SponsorLink to version 4.20 of Moq which included some things that a lot of people didn't like. I've used Moq for over a decade, it's a fantastic tool, and I'm going to stick with it for these demos. It's solid. It works, and most importantly, I know how it works -- but since I realise some of you might be using company laptops and don't want to get into All That, we'll lock Moq to version 4.18 which predates the controversial changes. If you aren't comfortable using Moq, for whatever reason, alternative .NET mocking libraries include [NSubstitute](https://nsubstitute.github.io/) and [https://fakeiteasy.github.io/](https://fakeiteasy.github.io/), and Moq itself is released under the BSD license so you're always free to fork it and run your own build. On the other hand, if you want to get into a discussion about sustainable open source funding, we can do that later over drinks.

Install Moq 4.18 from NuGet:

```
dotnet add package Moq --version 4.18
```















