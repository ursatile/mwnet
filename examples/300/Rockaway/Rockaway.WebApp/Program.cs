using Rockaway.WebApp.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
Log.Information("Serilog support enabled! Yeah!");
builder.Host.UseSerilog();

builder.Services.AddRazorPages();
builder.Services.AddSingleton<IStatusReporter>(new StatusReporter());
var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapGet("/status", (IStatusReporter reporter) => reporter.GetStatus());
app.Run();