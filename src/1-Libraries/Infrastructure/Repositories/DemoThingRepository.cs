// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure.DbContext;
using CodeBlock.DevKit.Infrastructure.Database;
using CodeBlock.DevKit.Core.Extensions;
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

    public async Task<long> CountAsync(string term)
    {
        var filter = GetFilter(term);
        return await _demoThings.CountDocumentsAsync(filter);
    }

    public async Task<IEnumerable<DemoThing>> SearchAsync(string term, int pageNumber, int recordsPerPage)
    {
        var filter = GetFilter(term);
        var sortDefinition = Builders<DemoThing>.Sort.Descending(u => u.CreationTime.DateTime);

        return await _demoThings
            .Find(filter)
            .Sort(sortDefinition)
            .Skip(recordsPerPage * (pageNumber - 1))
            .Limit(recordsPerPage)
            .ToListAsync();
    }

    private FilterDefinition<DemoThing> GetFilter(string term)
    {
        var filterBuilder = Builders<DemoThing>.Filter;
        var filters = new List<FilterDefinition<DemoThing>>();

        if (!term.IsNullOrEmptyOrWhiteSpace())
        {
            filters.Add(
                filterBuilder.Or(
                    filterBuilder.Regex(u => u.Name, new MongoDB.Bson.BsonRegularExpression(term, "i")),
                    filterBuilder.Regex(u => u.Description, new MongoDB.Bson.BsonRegularExpression(term, "i"))
                )
            );
        }

        return filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
    }
}
