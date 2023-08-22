using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IClock, SystemClock>();

var loggerFactory = LoggerFactory.Create(lb => lb.AddConsole());
var logger = loggerFactory.CreateLogger<Program>();

Action<DbContextOptionsBuilder> options;
var sqlite = builder.Configuration["database"] == "sqlite";
if (sqlite) {
	logger.LogInformation("Using SQLite database provider");
	var conn = new SqliteConnection("Data Source=rockaway;Mode=Memory;Cache=Shared");
	// open a SQLite connection that will stay open as long as
	// the app is running, to keep our in-memory database alive.
	await conn.OpenAsync();
	options = dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlite(conn);
} else {
	logger.LogInformation("Using MS SQL Server database provider");
	var connectionString = builder.Configuration.GetConnectionString("rockaway-mssql-server");
	options = dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(connectionString);
}

builder.Services.AddDbContext<RockawayDbContext>(options);
builder.Services
	.AddDefaultIdentity<IdentityUser>()
	.AddEntityFrameworkStores<RockawayDbContext>();

#if DEBUG
builder.Services.AddSassCompiler();
#endif

var app = builder.Build();
if (sqlite) {
	using var scope = app.Services.CreateScope();
	var db = scope.ServiceProvider.GetService<RockawayDbContext>()!;
	lock (db) db.Database.EnsureCreated();
}

app.UseRouting();

app.Use(next => context => {
	logger.LogInformation($"Routing: Found: {context.GetEndpoint()?.DisplayName}");
	return next(context);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Admin}/{action=Index}/{id?}");

app.UseStaticFiles();
app.Run();