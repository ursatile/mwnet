---
title: "2.3 Exercises"
layout: module
nav_order: 0203
typora-root-url: ./
typora-copy-images-to: ./images
summary: "Exercises using minimal APIs"
previous: mwnet202
---

So far in this module, we've set up an Azure continuous deployment pipeline, and added an HTTP endpoint that returns the status of our system.

### Exercise: Add Uptime

We want to see how long it is since our application was last deployed or restarted.

1. Add Uptime to the existing `/status` endpoint, as a human-readable string - e.g. `"142:28:46"` indicates 142 hours, 28 minutes and 46 seconds of uptime.
2. Add a new API endpoint at `/uptime` which returns a single number, being the number of milliseconds since the app was last restarted - so in the scenario above, the endpoint will return `512926`

Include endpoint tests for your uptime service.
