var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");
app.MapGet("/multiply/{x}/{y}", (int x, int y) => x * y);
app.MapGet("/multiply", (int x, int y) => x * y);

app.MapGet("/dotw/{date}", (DateTime? date) => (date ?? DateTime.Now).DayOfWeek.ToString());
app.MapPost("/result" , (HttpContext ctx) => {
    Int32.TryParse(ctx.Request.Form["x"], out int x);
    Int32.TryParse(ctx.Request.Form["y"], out int y);    
    return $"Result: {x * y}";
});

app.UseStaticFiles();

app.Run();
