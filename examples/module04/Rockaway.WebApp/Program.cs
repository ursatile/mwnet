using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;

const string SQL_CONNECTION_STRING =
	"Server=localhost;Database=rockaway;User Id=rockaway_user;Password=3GX9i0F5YPmsa6;TrustServerCertificate=true;";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlServer(SQL_CONNECTION_STRING));
builder.Services.AddRazorPages();
var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.MapRazorPages();
app.Run();
