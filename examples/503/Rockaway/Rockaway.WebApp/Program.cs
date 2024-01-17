using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Hosting;
using Rockaway.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(
	options => options.Conventions.AuthorizeAreaFolder("admin", "/")
);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IStatusReporter>(new StatusReporter());

var logger = CreateAdHocLogger<Program>();

logger.LogInformation("Rockaway running in {environment} environment", builder.Environment.EnvironmentName);
if (builder.Environment.UseSqlite()) {
	logger.LogInformation("Using Sqlite database");
	var sqliteConnection = new SqliteConnection("Data Source=:memory:");
	sqliteConnection.Open();
	builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlite(sqliteConnection));
} else {
	logger.LogInformation("Using SQL Server database");
	var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
	builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlServer(connectionString));
}

builder.Services.AddDefaultIdentity<IdentityUser>()
	.AddEntityFrameworkStores<RockawayDbContext>();

#if DEBUG
builder.Services.AddSassCompiler();
#endif

var app = builder.Build();

if (app.Environment.IsProduction()) {
	app.UseExceptionHandler("/Error");
	app.UseHsts();
} else {
	app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope()) {
	using var db = scope.ServiceProvider.GetService<RockawayDbContext>()!;
	if (app.Environment.UseSqlite()) {
		db.Database.EnsureCreated();
	} else if (Boolean.TryParse(app.Configuration["apply-migrations"], out var applyMigrations) && applyMigrations) {
		logger.LogInformation("apply-migrations=true was specified. Applying EF migrations and then exiting.");
		db.Database.Migrate();
		logger.LogInformation("EF database migrations applied successfully.");
		Environment.Exit(0);
	}
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapGet("/status", (IStatusReporter reporter) => reporter.GetStatus());
app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
).RequireAuthorization();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();

ILogger<T> CreateAdHocLogger<T>() {
	var config = new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", false, true)
		.AddEnvironmentVariables()
		.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
		.Build();
	return LoggerFactory.Create(lb => lb.AddConfiguration(config)).CreateLogger<T>();
}
