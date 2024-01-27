---
title: "6.1 Rendering Email"
layout: module
nav_order: 10601
typora-root-url: ./
typora-copy-images-to: ./images
summary: "In this module, we'll learn how to render HTML and text emails using ASP.NET, Razor, and the Mailjet Markup Language (MJML)."
previous: mwnet508
complete: mwnet601
examples: examples/601/Rockaway
---

We've chosen our tickets, we've entered our details... this is the part where we get the confirmation email, right? And, because we want to look like we know what we're doing, the email should probably have a nicely-formatted HTML version with some colors and pictures and stuff -- and because accessibility is a thing, we probably also need a clean, readable plain text version.

Before we even think about actually sending the email, let's figure out what it looks like and how we're going to build it. Our designer has provided this:

![image-20240127190704611](/images/rockaway-email-mockup.png)

## Rendering Email with Razor and MJML

HTML email is a nightmare. Gmail, Outlook, iOS Mail, Android Mail, various web mail clients, all with their own interpretations of which HTML tags they support. 

>  The Yahoo! Mail app on Android removes the `<head>` element from HTML email messages... but [only the first one](https://github.com/hteumeuleu/email-bugs/issues/28), so if you want to include styles in an HTML message, include two `<head>` elements and put anything important in the second one. That's the kind of idiosyncrasy you'll have to deal with if you're building HTML emails by hand.

Mailjet Markup Language ([mjml.io](https://mjml.io/)) makes it better: instead of writing horrible HTML, we can write MJML, and it'll turn it into HTML for us, so we write this:

```xml
{% include_relative examples/email-example.mjml %}
```

and we get this:

```html
{% include_relative examples/email-example.html %}
```

The good news: there's a [native port of MJML to .NET](https://github.com/SebastianStehle/mjml-net), which is [available on NuGet](https://www.nuget.org/packages/Mjml.Net), so we don't need to install nodeJS.

```dotnetcli
dotnet add package Mjml.Net
```

The bad news: MJML compiles to HTML. It's a transpiler: MJML in, HTML out. There's no way to put in a view model, or template data, or run loops, or logic, or anything.

So, what we're going to do is embed Razor syntax in our MJML, then compile MJML > HTML, hopefully with our Razor markup intact, and then pass the result to a Razor compiler along with a model containing our email data - customer name, artist, tickets, etc.

Here's the full workflow we'll end up with:

![mail-workflow](/images/mail-workflow.png)
<!--
Controller->MailSender: SendOrderConfirmation(order)
MailSender->Renderer: RenderHtml(order)
Renderer->Templates: Get MJML
Templates->Renderer: <MJML source>
Renderer->MJML: Render(mjml)
MJML->Renderer: Razor HTML
Renderer->RazorEngine: Compile HTML!
RazorEngine->Renderer: htmlTemplate
note left of Renderer: htmlTemplate.Run(order)
Renderer->MailSender: HtmlBody
MailSender->Renderer: RenderText(order)
Renderer->Templates: Get TXT
Templates->Renderer: <TXT source>
Renderer->RazorEngine: Compile TXT!
RazorEngine->Renderer: textTemplate
note left of Renderer: textTemplate.Run(order)
Renderer->MailSender: TextBody
note left of MailSender:
Create MimeMessage
.HtmlBody
.TextBody
end note
MailSender->SmtpRelay: Send(message)
SmtpRelay->MailSender: Message sent OK
MailSender->Controller: OK!
-->

Some things to bear in mind:

First, **this is a hack.** It is completely unsupported by the maintainers of the projects involved. When it works, it's glorious. When it doesn't, you aren't going to get useful error messages.

Second, **MJML will remove anything it doesn't recognise**. It preserves its own markup tags, and anything inside what's called an "ending tag" - [read more about ending tags here](https://documentation.mjml.io/#ending-tags).

What this means in practice is that we can do this:

```xml
<mjml>
	<mj-body>
		<mj-section>
			<mj-column>
				<mj-text>
					@foreach(var item in Model.Items) {
					<p>@item</p>
					}
				</mj-text>
			</mj-column>
		</mj-section>
	</mj-body>
</mjml>
```

but we **can't** do this:

```xml
<mjml>
	<mj-body>
		@foreach(var item in Model.Items) {
		<mj-section>
			<mj-column>
				<mj-text>
					<p>@item</p>
				</mj-text>
			</mj-column>
		</mj-section>
		}
	</mj-body>
</mjml>
```

The MailJet compiler wil see the `@foreach(var item in Model.Items) {`, but because it's between `<mj-body>` and `<mj-section>`, it's not valid content and so Mailjet will delete it. (Mailjet has no idea it's Razor code: it treats anything other than its own markup tags as either regular HTML, or invalid content).

### Compiling Razor Views using RazorEngine

To turn the resulting Razor code into populated HTML, we'll install [RazorEngineCore](https://www.nuget.org/packages/RazorEngineCore) from NuGet:

```dotnetcli
dotnet add package RazorEngineCore
```

Now, let's build some classes. 

We're going to store our mail templates as part of our project, and we'll set the compilation type to "Embedded Resource", so the template source becomes part of our application assembly -- that way there's no risk of files going missing during deployment.

We'll define an interface for the template provider:

```csharp
// Rockaway.WebApp/Services/Mail/IMailTemplateProvider.cs
{% include_relative {{ page.examples }}/Rockaway.WebApp/Services/Mail/IMailTemplateProvider.cs %}
```

Then create an implementation based on embedded resource files:

```csharp
// Rockaway.WebApp/Services/Mail/ResourceMailTemplateProvider.cs
{% include_relative {{ page.examples }}/Rockaway.WebApp/Services/Mail/ResourceMailTemplateProvider.cs %}
```
and another one for debugging, which recompiles everything on every request so we can refresh a page and see the results immediately:

```csharp
// Rockaway.WebApp/Services/Mail/DebugMailTemplateProvider.cs
{% include_relative {{ page.examples }}/Rockaway.WebApp/Services/Mail/DebugMailTemplateProvider.cs %}
```
The interface for the mail renderer:

```csharp
// Rockaway.WebApp/Services/Mail/IMailBodyRenderer.cs
{% include_relative {{ page.examples }}/Rockaway.WebApp/Services/Mail/IMailBodyRenderer.cs %}
```

and the implementation.

```csharp
// Rockaway.WebApp/Services/Mail/MailBodyRenderer.cs
{% include_relative {{ page.examples }}/Rockaway.WebApp/Services/Mail/MailBodyRenderer.cs %}
```

> As well as our sneaky Razor markup, there might be @ signs in the MJML code which refer to CSS rules, like `@media`. Before we compile our Razor template, we replace any matching rules like `@media` with `@@media` -- and then the Razor engine turns the `@@` back into a literal `@` in the final HTML.

## The Email Templates

The MJML version of the template is at `Rockaway.WebApp/Templates/Mail/OrderConfirmation.mjml`:

```xml
{% include_relative {{ page.examples}}/Rockaway.WebApp/Templates/Mail/OrderConfirmation.mjml %}
```

and the text version, which is actually a Razor view that just doesn't include any HTML tags, is at `Rockaway.WebApp/Templates/Mail/OrderConfirmation.txt`:

```
{% include_relative {{ page.examples}}/Rockaway.WebApp/Templates/Mail/OrderConfirmation.txt %}
```

## Previewing Emails

Finally, let's wire up a TicketOrdersController in the Admin area, and add some code which will let use preview the emails for a particular order right in a browser, so we can check they render OK -- and more importantly, fix them if they don't without having to send an email every time we change anything to see if it worked.

We'll use the aspnet-codegenerator to create a TicketOrdersController:

```

```





