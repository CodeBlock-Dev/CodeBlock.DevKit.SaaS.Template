// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure.DbContext;
using CodeBlock.DevKit.Infrastructure.Database;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.Repositories;

internal class DemoThingRepository : MongoDbBaseAggregateRepository<DemoThing>, IDemoThingRepository
{
    private readonly IMongoCollection<DemoThing> _demoThings;

    public DemoThingRepository(MainDbContext dbContext)
        : base(dbContext.DemoThings)
    {
        _demoThings = dbContext.DemoThings;
    }

    public Task<long> CountAsync(string term) => throw new NotImplementedException();

    public Task<IEnumerable<DemoThing>> SearchAsync(string term, int pageNumber, int recordsPerPage) => throw new NotImplementedException();
}
