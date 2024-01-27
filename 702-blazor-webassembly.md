---
title: "7.2 Razor Components and WebAsssembly"
layout: module
nav_order: 10508
typora-root-url: ./
typora-copy-images-to: ./images
summary: "Let's add tickets and ticket prices"
previous: mwnet701
complete: mwnet702
examples: examples/702/Rockaway
---

In the last module, we created an interactive `TicketPicker`, a Razor component that provides immediate feedback and a rich client experience when customers are selecting gig tickets.

It worked great, but the interactivity relied on making network calls to the server -- behind the scenes, whenever you click a button, ASP.NET is using SignalR to send a message to the server, run the code, get the modified HTML, and update the state of the controls on the page.

In this module, we'll see what we need to do to deploy our TicketPicker as a component that runs directly in the browser using web assembly (WASM).

## Razor Component Render Modes

A Razor Component ends up on your browser screen in one of five different ways, known as **[render modes](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0)**:

**Static:** the control is rendered on the server. The client gets static HTML, and you're done -- no interactions, no behaviour. 

```
@(await Html.RenderComponentAsync<MyComponent>(RenderMode.Static))
```

**Server:** the page sends a placeholder to the client; the client then sends a network request to the server, fetches the state of the component, and renders it into the placeholder. Clients get a "loading flicker" - they'll see an empty placeholder for a moment or two while the client fetches the initial state of the component.


```
@(await Html.RenderComponentAsync<MyComponent>(RenderMode.Server))
```

**ServerPrerendered:** the page sends rendered HTML to the client; the client then sends a network request to the server, fetches the state of the component, and replaces the rendered HTML.

```
@(await Html.RenderComponentAsync<MyComponent>(RenderMode.ServerPrerendered))
```

**WebAssembly:** The entire control is delivered as client-side web assembly code, and nothing appears until the browser has downloaded all the assets, initialised the Blazor web assembly runtime, and initialised the control.

```
@(await Html.RenderComponentAsync<MyComponent>(RenderMode.WebAssembly))
```

**WebAssemblyPrerendered**: yep, you guessed it. Static HTML to start with, that gets replaced by client-side WASM code as soon as it's available.

```csharp
@(await Html.RenderComponentAsync<MyComponent>(RenderMode.WebAssemblyPrerendered))
```

We're going to turn our `TicketPicker` into a standalone component that will support any rendering style (although **static** won't do anything useful).



