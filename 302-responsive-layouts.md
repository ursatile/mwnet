---
title: 3.2 Responsive Layouts
layout: module
nav_order: 0302
summary: >-
  In this module, we'll use SCSS and Razor to apply a responsive layout, colors and UX design to our artists page.
examples: examples/302/rockaway/
typora-root-url: .
typora-copy-images-to: ./images
previous: mwnet301
complete: mwnet302
---

## Creating Response Layouts with SCSS

OK, let's make stuff look good.

First, we're going to import a **CSS reset**. Josh Comeau describes this as "sanding down some of the rough edges in the CSS language", which I think is a rather nice way to put it; it'll give us a set of stable baseline styles that we can extend as we need to to create our own look and feel.

We're going to use [Josh's custom CSS reset](https://www.joshwcomeau.com/css/custom-css-reset/#our-finished-product-10) for this (and we're going to strip out rule #8 about root stacking contexts, because that's mainly for folks working with React and similar frameworks, and we're not doing that.)

**Rockaway.WebApp/Styles/reset.css:**

```css
/*
  Josh's Custom CSS Reset
  https://www.joshwcomeau.com/css/custom-css-reset/
*/
*, *::before, *::after {
  box-sizing: border-box;
}
* {
  margin: 0;
}
body {
  line-height: 1.5;
  -webkit-font-smoothing: antialiased;
}
img, picture, video, canvas, svg {
  display: block;
  max-width: 100%;
}
input, button, textarea, select {
  font: inherit;
}
p, h1, h2, h3, h4, h5, h6 {
  overflow-wrap: break-word;
}
```

Update **styles.scss** to import the reset. We'll also add a rule to restrict the `body` width:

```scss
@import 'reset.css';

$foreground-color: #fff;
$background-color: #213;
$font-family: Arial, Helvetica, sans-serif;

html {
	color: $foreground-color;
	background: $background-color;
	font-family: $font-family;
}

body {
	max-width: 960px;
	margin: 0px auto;
}
```

Next, we're going to add something called a **mixin**. Mixins are a SASS feature that allows us to define a reusable rule. We're going to create a mixin called a `fancy-panel`, which creates a panel with rounded corners and a background colour:

```scss
@mixin fancy-panel($panel-color: $panel-background-color) {
	margin: $gutter 0;
	border-radius: $border-radius;
	padding: 2px + $border-radius;
	background-color: $panel-color;
}
```

Now, we're going to create CSS rules for our `header`, `main`, and `footer` elements, using this mixin:

```scss
body {
	max-width: 960px;
	margin: 0 auto;
	padding: 0 $gutter;

	> header {
		@include fancy-panel(#f00);
	}

	> main {
		@include fancy-panel;
	}

	> footer {
		@include fancy-panel(#0f0);
	}
}
```

![image-20230821144912710](/images/image-20230821144912710.png)

OK, the colour scheme isn't terribly tasteful -- but you get the idea :)

Let's use SASS variables and colour arithmetic to create a more harmonious colour scheme.

We're going to base our scheme on a single `brand-color`, and vary everything else based on this.

We need to import the color module right the top of our CSS, and declare a new variable `$brand-color`:

```scss
@use "sass:color";
@import 'reset.css';

$foreground-color: #fff;
$brand-color: #f36;
$background-color: #000;
$border-radius: 8px;
$gutter: 8px;
```

Then we're going to use `$brand-color` along with the `color.scale` function to automatically generate a monochromatic color palette based on variations of our chosen color, and style various elements in our page using this color.

```scss
// Check whether we're using a light or dark background:
$dark-mode: color.lightness($background-color) < 50%;

html {
	color: $foreground-color;
	background: $brand-color;
	background: linear-gradient(150deg, color.scale($brand-color, $lightness: -75%) 0%, color.scale($brand-color, $lightness: +15%) 50%, color.scale($brand-color, $lightness: -25%) 100%);
	background-attachment: fixed;
	font-family: Arial, Helvetica, sans-serif;
}

body {
	max-width: 960px;
	margin: 0px auto;
	padding: 0 $gutter;

	> header {
		@include fancy-panel;
	}

	> main {
		@include fancy-panel;
	}

	> footer {
		@include fancy-panel;
	}

	a {
		$amount: if($dark-mode, +75%, -75%);
		color: color.scale($brand-color, $lightness: $amount);
		text-decoration: none;
	}
}
```

> Notice the conditional expression `if($dark-mode, +75%, -75%)` in the rule controlling the color of `a` elements

Now, see what happens to the page when we change the value of `$brand-color` to different CSS color values.

**$brand-color: #036;**

![image-20230821162935776](/images/image-20230821162935776.png)

**$brand-color: #906;**

![image-20230821163026988](/images/image-20230821163026988.png)

**brand-color: #9c9;**

![image-20230821163055645](/images/image-20230821163055645.png)

Try modifying the `$foreground-color` and `$background-color` variables as well:

## Setting a Site Logo

Our site "logo" is currently an `<h1>` element inside the `<header>`. This is brilliant for accessibility, search engines, screen readers -- almost anything which respects semantic markup will treat a nice big H1 at the top of the page header as a very important indicator of what the page is about.

It looks a bit cheap, though. Our designers have created a Rockaway company [logotype](https://99designs.com/blog/logo-branding/logotype-vs-logomark-vs-logo/), which they've sent us as an SVG file:

[rockaway-logotype.svg](examples/302/rockaway/Rockaway.WebApp/wwwroot/img/rockaway-logotype.svg)

![Rockaway logotype](examples/302/rockaway/Rockaway.WebApp/wwwroot/img/rockaway-logotype.svg)

We're going to replace that H1 with the site logotype, and we're going to do it using pure CSS. 

We'll apply a layout rule to the `header` so that it'll render using a CSS `flex` layout:

> CSS Flexbox offers far more options and capabilities than we have time to explore in this workshop. If you want to know more about it, check out [A Complete Guide to Flexbox](https://css-tricks.com/snippets/css/a-guide-to-flexbox/) at css-tricks.com

```scss
body {
	> header {
		@include fancy-panel;
		display: flex;
		justify-content: space-between;
        
		h1 {
			font-size: 0;
			height: 38px;
			width: 200px;
			background: url(/img/rockaway-logotype.svg) left center no-repeat;
		}
	}	
}
```



![image-20230821165127162](/images/image-20230821165127162.png)

Nice.

OK, let's style the site navigation menu:

```scss
body {
	> header {
    	nav {
            a {
                background: $brand-color;
                display: inline-block;
                padding: 4px 16px;
                color: #fff;
                font-size: 120%;
                border-radius: $border-radius/2;

                &:hover {
                    background: color.scale($brand-color, $lightness: +20%);
                }
            }
        }
	}
}
```

and while we're here, we'll apply a flex layout and style the footer element as well:

```scss
> footer {
    @include fancy-panel;		
    font-size: 80%;
    display: flex;
    justify-content: space-between;
    nav {
        a {
            display: inline-block;
            border-left: 1px solid #fff;
            padding: 0 0.5em;
        }

        a:first-child {
            border: none;
        }

        a:hover {
            text-decoration: underline;
        }
    }
}
```

![image-20230821165455475](/images/image-20230821165455475.png)

## Web Typography

OK, we've got a layout, we've got a color scheme, we've got navigation. Let's mix up our fonts a little bit.

Let's set our website in **PT Sans Narrow**, a [free font available on Google Fonts](https://fonts.google.com/specimen/PT+Sans+Narrow?query=pt+sans+nar).

We'll need to `@import` Google's font definition into our SCSS file, declare a `$font-family` variable that references this font (and some sensible fallback fonts in case this one won't load!), and then reference `$font-family` in our CSS rule:

```scss
@import url('https://fonts.googleapis.com/css2?family=PT+Sans+Narrow:wght@400;700&display=swap');

$font-family: 'PT Sans Narrow', Arial, helvetica, sans-serif;

html {
    	font-family: $font-family;
}
```

## Dynamic navigation for mobile devices

Because we've used a CSS flex layout for our site header, if we open our site on a mobile device, it'll look a little weird.

![image-20230821170231703](/images/image-20230821170231703.png)

The important thing to notice here is that **it still works**. All our navigation links are visible, and if we click on them, the right thing happens. However, we're wasting a lot of screen space here. Let's see how we can build a responsive drop-down navigation menu using only HTML and CSS -- no JavaScript required.

To do this, we need to remember three things about how browsers work:

1. We can attach a <label> element to an HTML `<input type="checkbox">`, so that whenever anybody clicks the label, it toggles the state of the checkbox
2. A `<label>` can contain just about any other markup - so we can put whatever we like in our toggle button.
3. CSS allows us to query the state of a checkbox - `checked` or `unchecked` - and change the appearance of other elements on the page based on this.

### Mixins and @media selectors

First, we're going to define a breakpoint, so that our responsive navigation only kicks in when the window is narrower than a specified threshold.

We can do this using a SASS mixin:

```
@mixin smartphone {
	@media (max-width: 480px) {
		@content;
	}
}
```

Now, we'll use that mixin to hide our `<nav>` if we're viewing our page on a smartphone:

```
nav {
    @include smartphone {
        display: none;
    }
    a {
        background: $brand-color;
        display: inline-block;
        padding: 4px 16px;
        color: #fff;
        font-size: 120%;
        border-radius: $border-radius/2;

        &:hover {
            background: color.scale($brand-color, $lightness: +20%);
        }
    }
}
```

Next, we'll add a label to our `<header>`, and an associated `<input type="checkbox" />`

```html
	<header>
		<a href="/">
			<h1>
				Rockaway
			</h1>
		</a>
		<label id="nav-toggle" for="nav-checkbox">NAV</label>
		<input type="checkbox" id="nav-checkbox" />
		<nav>
			<a href="/">home</a>
			<a asp-page="/artists">artists</a>
			<a asp-page="/venues">venues</a>
		</nav>
	</header>
```

and CSS rules which use the `smartphone` mixin to control display of these elements:

```scss
body {
	> header {
		label#nav-toggle {
			display: none;
			@include smartphone {
				display: block;
				position: absolute;
				z-index: 2;
				top: $gutter;
				right: $gutter;
			}
		}

		input#nav-checkbox {
			height: 0;
			@include smartphone {
				&:checked ~ nav {
					height: 300px;
				}
			}
		}

    }
}    
```

Finally, we plug in a set of styles for our `nav` element that control how it appears on mobile devices:

```scss
body {
    > header {
	    nav {
            /* existing styles omitted */
            @include smartphone {
                position: absolute;
                overflow-y: hidden;
                display: block;
                top: 0;
                left: 0;
                right: 0;
                height: 0;
                transition: height 0.2s ease;
                a {
                    display: block;
                    width: 100%;
                    padding: 16px;
                    border-radius: 0;
                    border-top: 1px solid $foreground-color;
                }
			}
		}
	}
}
```

And there it is: we've created a responsive nav menu using only HTML and CSS:

![image-20230821182626073](/images/image-20230821182626073.png)

## FontAwesome

One final thing left to tweak: the label at the moment says NAV. 

I'd like that to be one of those three-row "hamburger menu" icons that's become an established convention for signifying mobile navigation.

![image-20230827124104065](/images/image-20230827124104065.png)

<caption>"Click on the hamburger menu"</caption>

We *could* build a pseudo-icon by styling up some `<span> tags or something, but instead we're going to take this opportunity to install a library called FontAwesome, which we'll be using throughout the rest of the workshop.

FontAwesome uses web font technology to add responsive icons and images to our web UI.

First, we'll install FontAwesome into our project.

There's a package on nuget.org called FontAwesome. It's seven years old and completely useless. Don't use it.

Instead, we're going to download the "Free For Web" bundle from [https://fontawesome.com/download](https://fontawesome.com/download)

1. Download [fontawesome-free-6.4.2-web.zip](https://use.fontawesome.com/releases/v6.4.2/fontawesome-free-6.4.2-web.zip)
2. Open up the ZIP file and copy:
   1. Everything from `/font-awesome-free-6.4.2-web/scss/` into a **new folder** `Rockaway.WebApp/Styles/FontAwesome/`
   2. Everything from `/font-awesome-free-6.4.2-web/webfonts/` into a **new folder** `Rockaway.WebApp/wwwroot/webfonts/`

Now, open up `styles.scss` and add these lines at the top, just below our other `@import` statements:

```scss
@import 'FontAwesome/fontawesome.scss';
@import 'FontAwesome/solid.scss';
@import 'FontAwesome/brands.scss';
```

To check FontAwesome is working properly, try adding a snippet of markup like this to one of your Razor views or pages:

```html
<h2>FontAwesome examples:</h2>
<p>
    <i class="fa-solid fa-hand-spock"></i>
    <i class="fa-solid fa-compact-disc"></i>
    <i class="fa-solid fa-guitar"></i>
    <i class="fa-solid fa-otter"></i>
    <i class="fa-solid fa-jedi"></i>
    <i class="fa-solid fa-stroopwafel"></i>
    <i class="fa-solid fa-user-astronaut"></i>
</p>
```

If you get a little gallery of icons like this:

![image-20230821221841365](/images/image-20230821221841365.png)

then it's working!

OK, back to our nav menu. The icon we're going to use is called `bars`:

<svg xmlns="http://www.w3.org/2000/svg" height="3em" viewBox="0 0 448 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M0 96C0 78.3 14.3 64 32 64H416c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 128 0 113.7 0 96zM0 256c0-17.7 14.3-32 32-32H416c17.7 0 32 14.3 32 32s-14.3 32-32 32H32c-17.7 0-32-14.3-32-32zM448 416c0 17.7-14.3 32-32 32H32c-17.7 0-32-14.3-32-32s14.3-32 32-32H416c17.7 0 32 14.3 32 32z"/></svg>

[https://fontawesome.com/icons/bars?f=classic&s=solid](https://fontawesome.com/icons/bars?f=classic&s=solid)

so we'll edit the `<header>` in our `_Layout.cshtml`:

```html
<label id="nav-toggle" for="nav-checkbox">
    <i class="fa-solid fa-bars"></i>
</label>
```

and then tweak our `styles.scss` to whack up the font size a bit:

```scss
body {
	> header {
		label#nav-toggle {
			@include smartphone {
				font-size: 200%;
			}
		}
	}
}
```

## Testing on real devices using NGrok

That looks pretty good... but is it going to look good on a real mobile device?

Testing page layouts on real devices can often give you an early heads-up about all kinds of layout and usability issues, and those sorts of problems are much easier to fix at this early stage -- so how can we get a real mobile phone browsing localhost?

You *could* connect your phone to the same WiFi network as your development machine, use `ipconfig` to find your local IP address:

``` bash
Windows IP Configuration

Ethernet adapter Ethernet:

   Connection-specific DNS Suffix  . :
   Link-local IPv6 Address . . . . . : fe80::e4ee:4b90:78bb:8d76%6
   IPv4 Address. . . . . . . . . . . : 192.168.0.107
   Subnet Mask . . . . . . . . . . . : 255.255.255.0
   Default Gateway . . . . . . . . . : 192.168.0.1
```

Then disable your Windows firewall 🤫 and then run your website with:

```
dotnet run --urls=http://*:5000/
```

and then browse to http://192.168.0.107:5000/ 

Alternatively, you can use a tool called **ngrok** - "secure tunnels to localhost"

Download ngrok from [ngrok.com](https://ngrok.com/), and unzip **ngrok.exe** to somewhere you can run it.

> I use the Cygwin tools on all my development machines, and install them into `C:\Windows\Cygwin`, so every Windows machine I use has a folder called `C:\Windows\Cygwin\bin` which is on my system `PATH`. Anything I want to run from the command line, I copy into `C:\Windows\Cygwin\bin`.

Start your web app, watch for port it's listening on:

![image-20230821224012701](/images/image-20230821224012701.png)

and then in a separate window, run `ngrok http ` and specify the port number your app is using. Once it's connected, you'll have a real live internet address -- ` https://d81f67a722fd.ngrok.app` in this example -- which you can use to browse your app running on localhost:

![image-20230821224348036](/images/image-20230821224348036.png)

One more tip - don't type that URL in! Copy & paste it into an online QR code generator, like [this one](https://ngrok-qr.alanwsmith.com/), and scan the code with your phone camera.

> Get into the habit of checking your designs on a real device every time you create a new page layout, or make a change to your site's CSS.

Based on testing on a real smartphone, I'm going to make the font size a little bigger on mobile devices. I'm using a CSS expression called `max` here, and `1em` is the "default" font size, so what this rule actually says is "hey, if the font would end up smaller than 20px, make it 20px, otherwise don't change it":

```scss
body {
    @include smartphone {
		font-size: max(1em, 20px);
	}
}
```

## Recap and Review

In this module, we've:

* Used a CSS reset to circumvent some of the quirks of browsers' built-in rendering styles
* Learned how to use SCSS mixins and media queries to create responsive layouts
* Created a pure HTML and CSS navigation menu that works on mobile devices
* Added FontAwesome to our project so we can use font-based icons
* Used ngrok to create tunnels to localhost so we can browse our development code from a real mobile device.





















