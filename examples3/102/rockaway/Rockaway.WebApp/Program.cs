var builder = WebApplication.CreateBuilder(args);

// Add support for Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Add routing support used by Razor Pages
app.UseRouting();
// Map requests to Razor pages
app.MapRazorPages();

// app.MapGet("/", () => "Hello World!");

app.Run();
