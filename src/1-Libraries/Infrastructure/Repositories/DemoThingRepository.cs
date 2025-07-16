// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure.DbContext;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
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

    public async Task<long> CountAsync(string term, DemoThingType? type, DateTime? fromDateTime, DateTime? toDateTime)
    {
        var filter = GetFilter(term, type, fromDateTime, toDateTime);
        return await _demoThings.CountDocumentsAsync(filter);
    }

    public async Task<IEnumerable<DemoThing>> SearchAsync(
        string term,
        DemoThingType? type,
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        DateTime? fromDateTime,
        DateTime? toDateTime
    )
    {
        var filter = GetFilter(term, type, fromDateTime, toDateTime);

        var sortDefinition =
            sortOrder == SortOrder.Desc
                ? Builders<DemoThing>.Sort.Descending(u => u.CreationTime.DateTime)
                : Builders<DemoThing>.Sort.Ascending(u => u.CreationTime.DateTime);

        return await _demoThings.Find(filter).Sort(sortDefinition).Skip(recordsPerPage * (pageNumber - 1)).Limit(recordsPerPage).ToListAsync();
    }

    private FilterDefinition<DemoThing> GetFilter(string term, DemoThingType? type, DateTime? fromDateTime, DateTime? toDateTime)
    {
        var filterBuilder = Builders<DemoThing>.Filter;
        var filters = new List<FilterDefinition<DemoThing>>();

        if (type.HasValue)
            filters.Add(filterBuilder.Eq(u => u.Type, type.Value));

        if (fromDateTime.HasValue)
            filters.Add(filterBuilder.Gte(t => t.CreationTime.DateTime, fromDateTime.Value.ToUniversalTime()));

        if (toDateTime.HasValue)
            filters.Add(filterBuilder.Lte(t => t.CreationTime.DateTime, toDateTime.Value.ToUniversalTime()));

        if (!term.IsNullOrEmptyOrWhiteSpace())
        {
            filters.Add(
                filterBuilder.Or(
                    filterBuilder.Regex(u => u.Name, new MongoDB.Bson.BsonRegularExpression(term, "i")),
                    filterBuilder.Regex(u => u.Description, new MongoDB.Bson.BsonRegularExpression(term, "i")),
                    filterBuilder.Regex(u => u.UserId, new MongoDB.Bson.BsonRegularExpression(term, "i"))
                )
            );
        }

        return filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
    }
}
