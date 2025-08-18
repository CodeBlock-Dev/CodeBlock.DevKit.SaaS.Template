using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Infrastructure.Database;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.DbContext;

/// <summary>
/// Main database context for the application that extends MongoDbContext to provide
/// MongoDB-specific functionality. This class demonstrates how to set up a database context
/// with collections, indexes, and custom database operations.
/// 
/// IMPORTANT: This is an example implementation for learning purposes. Replace DemoThing
/// with your actual domain entities and collections.
/// 
/// Key features demonstrated:
/// - MongoDB collection management
/// - Database index creation
/// - Safe test database operations
/// - Collection access through properties
/// </summary>
internal class MainDbContext : MongoDbContext
{
    /// <summary>
    /// Initializes a new instance of MainDbContext with MongoDB settings.
    /// </summary>
    /// <param name="mongoDbSettings">MongoDB connection and configuration settings</param>
    public MainDbContext(MongoDbSettings mongoDbSettings)
        : base(mongoDbSettings) { }

    /// <summary>
    /// MongoDB collection for DemoThing entities.
    /// This property provides access to the DemoThings collection for CRUD operations.
    /// </summary>
    public IMongoCollection<DemoThing> DemoThings { get; private set; }

    /// <summary>
    /// Creates database indexes for optimal query performance.
    /// This method demonstrates how to set up indexes on commonly queried fields.
    /// 
    /// Example: Creates a non-unique index on the Name field for faster text searches.
    /// </summary>
    protected override void CreateIndexes()
    {
        DemoThings.Indexes.CreateOne(
            new CreateIndexModel<DemoThing>(
                Builders<DemoThing>.IndexKeys.Ascending(x => x.Name),
                new CreateIndexOptions() { Name = nameof(DemoThing.Name), Unique = false }
            )
        );
    }

    /// <summary>
    /// Safely drops test databases for testing purposes.
    /// Only drops databases that start with "Test_" prefix to prevent accidental
    /// deletion of production databases.
    /// 
    /// Example usage in test cleanup:
    /// dbContext.DropTestDatabase();
    /// </summary>
    public void DropTestDatabase()
    {
        // Only drop the database if it starts with "Test_" to avoid dropping production databases.
        if (!_mongoDbSettings.DatabaseName.StartsWith("Test_"))
            return;

        _client.DropDatabase(_mongoDbSettings.DatabaseName);
    }
}
