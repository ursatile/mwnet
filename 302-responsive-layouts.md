---
title: 3.1 Responsive Layouts
layout: module
nav_order: 0302
summary: >-
  In this module, we'll use SCSS and Razor to apply a responsive layout, colors and UX design to our artists page.
examples: examples/301/rockaway/
typora-root-url: .
typora-copy-images-to: ./images
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
@mixin fancy-panel($bg-color: $panel-background-color) {
	margin: $gutter 0;
	border-radius: $border-radius;
	padding: 2px + $border-radius;
	background-color: $bg-color;
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

