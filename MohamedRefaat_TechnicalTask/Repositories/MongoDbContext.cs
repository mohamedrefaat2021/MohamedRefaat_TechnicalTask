using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

public class MongoDbContext
{
    public IMongoDatabase Database { get; }

    public MongoDbContext(IConfiguration configuration)
    {
        // Ensure the connection string and database name are present in the configuration
        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
        {
            throw new ArgumentException("MongoDB ConnectionString and DatabaseName must be configured.");
        }

        // Initialize MongoClient with the connection string
        var client = new MongoClient(connectionString);
        Database = client.GetDatabase(databaseName);
    }
}
