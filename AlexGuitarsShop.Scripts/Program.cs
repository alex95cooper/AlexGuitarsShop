using AlexGuitarsShop.Scripts;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
SeedDatabase.Init(connectionString);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();