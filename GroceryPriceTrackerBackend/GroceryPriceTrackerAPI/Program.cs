using GroceryPriceTrackerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Register MongoDBService as a singleton
builder.Services.AddSingleton<MongoDBService>();

// Register HttpClient as a service
builder.Services.AddHttpClient();

// Register controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
