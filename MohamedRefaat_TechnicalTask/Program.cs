using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CsvRepository>(provider =>
{
    var filePath = builder.Configuration["CsvFilePath"];
    return new CsvRepository(filePath);
});
builder.Services.AddScoped<MongoRepository>(provider =>
{
    var connectionString = builder.Configuration["MongoDb:ConnectionString"];
    var databaseName = builder.Configuration["MongoDb:DatabaseName"];
    var collectionName = builder.Configuration["MongoDb:CollectionName"];

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase(databaseName);
    return new MongoRepository(database, collectionName);
});

builder.Services.AddScoped<IPersonService, PersonService>();
// Register CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")  // Update with the frontend URL
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});



var app = builder.Build();

// Enable CORS
app.UseCors(); // Use the CORS policy

// Serve static files from the "UI" folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "UI")),
    RequestPath = "" // Serve files without needing "/UI/" in the URL
});

// Configure routing
app.UseRouting();
app.UseAuthorization();

// Fallback route to serve index.html for unmatched paths
app.MapFallbackToFile("index.html");

app.MapControllers();
app.Run();
