var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.MapPost("/info", (object o) => { });
app.Run();
