// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Infrastructure.Database;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.Repositories;

internal class DemoThingRepository : MongoDbBaseAggregateRepository<DemoThing>, IDemoThingRepository
{
    private readonly IMongoCollection<DemoThing> _settings;

    public DemoThingRepository(DemoThingsDbContext dbContext)
        : base(dbContext.AppDemoThings)
    {
        _settings = dbContext.AppDemoThings;
    }

    public async Task<DemoThing> GetAsync()
    {
        return await _settings.Find(x => true).FirstOrDefaultAsync();
    }

    public DemoThing Get()
    {
        return _settings.Find(x => true).FirstOrDefault();
    }
}
