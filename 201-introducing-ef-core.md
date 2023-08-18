---
title: 2.1 Introducing EF Core
layout: module
nav_order: 0201
summary: >-
  In this module we'll introduce Entity Framework (EF) Core and see how to use it to connect our web app to a relational database. We'll create our first database context object, and learn how to create and manage our application database.
examples: examples/201/rockaway/
typora-root-url: .
typora-copy-images-to: ./images
---

In this module, we're going to start adding some application data to our web app. The business scenario we're modelling here is tickets to rock concerts, so the first thing we're going to add here is a list of artists -- bands and musicians who are going to be performing at the shows we're selling tickets for.

First, though, we need to set up some infrastructure so we actually have a database to connect to.

## Running SQL Server using Docker

Docker is a platform for running virtualised applications. We're going to use Docker to create and run a stripped-down virtual machine known as a **container** which will host a local version of Microsoft SQL Server.

{: .note }
You'll need [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed to use the examples in this section. You could also connect to a regular database running on a local or remote installation of Microsoft SQL Server, but the instructions in this handbook assume you're running Docker.

To download and the latest SQL Server 2022 image from Docker:

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=p@ssw0rd" -p 1433:1433 --name rockaway-mssql-server -d mcr.microsoft.com/mssql/server:2022-latest
```

This will pull  the latest SQL Server 2022 image from Microsoft's Docker image repo, and start a new instance:

* `-e "ACCEPT_EULA=Y"` will automatically accept the End User License Agreement (required to run SQL Server)
* `-e "SA_PASSWORD=p@ssw0rd"` will set the `sa` password to `p@ssw0rd`
  * `sa` is the *system administrator* account built in to SQL Server. We're using `sa` here to connect to our new server instance and set up a database

* `-p 1433:1433` will map port 1433 on `localhost` to port 1433 on the Docker host.
  * 1433 is the default network port used by SQL Server.

* `--name rockaway-mssql-server` assigns a name to our host, which we'll use in the next step to run SQL commands on that host.

Use `docker container list` to see a list of running containers and check that our instance has started correctly:

```bash
D:\>docker container list
CONTAINER ID   IMAGE                                        COMMAND                  CREATED         STATUS         PORTS                    NAMES
dbc13666dc93   mcr.microsoft.com/mssql/server:2022-latest   "/opt/mssql/bin/perm…"   5 minutes ago   Up 5 minutes   0.0.0.0:1433->1433/tcp   rockaway-mssql-server
```

Next, we'll run a SQL script to create a user, set up an empty database, and add our new user to the `db_owner` role in that database. The script is available at [create-rockaway-database.sql](examples/create-rockaway-database.sql):

```sql
-- create-rockaway-database.sql

{% include_relative examples/201/create-rockaway-database.sql %}
```

To copy the script into our Docker container and run it:

```powershell
docker cp create-rockaway-database.sql rockaway-mssql-server:/opt/create-rockaway-database.sql

docker exec -it rockaway-mssql-server /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P p@ssw0rd -i /opt/create-rockaway-database.sql
```

You should get a response something like:

```
Microsoft SQL Server 2022 (RTM) - 16.0.1000.6 (X64)
	Oct  8 2022 05:58:25
	Copyright (C) 2022 Microsoft Corporation
	Developer Edition (64-bit) on Linux (Ubuntu 20.04.5 LTS) <X64>
Changed database context to 'rockaway'.
Adding user [rockaway_user] to database [rockaway]
Done.
Adding user [rockaway_user] to role [db_owner] in [rockaway] database
Done
```

{: .warning }

> If you're using an Apple Mac with the new Apple Silicon M1 or M2 processors, none of this will work, because there isn't an officially supported SQL Server image for the ARM64 architecture used in the M1/M2 Macs. 
>
> You can run the [Azure SQL Edge](https://hub.docker.com/_/microsoft-azure-sql-edge) Docker image on ARM64 Macs using this Docker command:
>
> ```bash
> docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=p@ssw0rd' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge
> ```
>
> That will give you a SQL database instance, but the ARM64 version of SQL Edge doesn't include the `sqlcmd` tool -- so even if you can get it to start, you'll need to connect from a tool like DataGrip and run the SQL script to create the database manually.
>
> Yay progress.

### Creating a DbContext

EF Core manages all data access through something called a DbContext. 

Install the NuGet package that adds EF Core and SQL Server support to our application:

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Next, create the `RockawayDbContext` we'll use in our Rockaway app:

**Rockaway.WebApp/Data/RockawayDbContext.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Data/RockawayDbContext.cs %}
```

Next, we'll modify `Program.cs` and register `RockawayDbContext` as a service.

**Rockaway.WebApp/Program.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Program.cs %}
```

We'll also add a few new namespaces to **Rockaway.,WebApp/GlobalUsings.cs:**

```csharp
{% include_relative examples/201/rockaway/Rockaway.WebApp/GlobalUsings.cs %}
```

We need to add a database connection string to our app configuration -- we'll add this to the `appSettings.Development.json` file so that it'll only apply when we're running our app in `Development` mode.

```json
{% include_relative examples/201/rockaway/Rockaway.WebApp/appsettings.Development.json %}
```

{: .note }
The connection string we're using here includes `TrustServerCertificate=true` -- this is required to connect to the SQL instance running in our Docker container, because the certificate that's used to encrypt our database connection isn't recognised as valid by Windows. For local development, this is fine, but in production, you should never ignore or override a certificate warning like this.

## Checking the Database Connection

Let's add a database connection check to our `/status` endpoint. Notice that to do this, we're adding a new constructor parameter so we can inject a `RockawayDbContext` into the `StatusController`:

**Rockaway.WebApp/Models/SystemStatus.cs:**

```
{% include_relative {{ page.examples }}Rockaway.WebApp/Models/SystemStatus.cs %}
```

**Rockaway.WebApp/Views/Status/Index.cshtml:**

```html
{% include_relative {{ page.examples }}Rockaway.WebApp/Views/Status/Index.cshtml %}
```

**Rockaway.WebApp/Controllers/StatusController.cs:**

```csharp
{% include_relative {{ page.examples }}Rockaway.WebApp/Controllers/StatusController.cs %}
```

Now, when we open our status page, we should see something like this:

![image-20230818115413775](/images/image-20230818115413775.png)

