using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Infrastructure.Database;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.DbContext;

internal class MainDbContext : MongoDbContext
{
    public MainDbContext(MongoDbSettings mongoDbSettings)
        : base(mongoDbSettings) { }

    public IMongoCollection<DemoThing> DemoThings { get; private set; }

    protected override void CreateIndexes()
    {
        DemoThings.Indexes.CreateOne(
            new CreateIndexModel<DemoThing>(
                Builders<DemoThing>.IndexKeys.Ascending(x => x.Name),
                new CreateIndexOptions() { Name = nameof(DemoThing.Name), Unique = false }
            )
        );
    }
}
