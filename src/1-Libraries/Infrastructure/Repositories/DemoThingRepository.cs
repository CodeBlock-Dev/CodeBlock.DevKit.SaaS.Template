// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure.DbContext;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Infrastructure.Database;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.Repositories;

/// <summary>
/// MongoDB repository implementation for DemoThing domain entities.
/// This class demonstrates how to implement a repository pattern with MongoDB,
/// extending the base repository with custom query methods for specific business requirements.
/// 
/// IMPORTANT: This is an example implementation for learning purposes. Replace DemoThing
/// with your actual business domain entities and implement your specific query requirements.
/// 
/// Key features demonstrated:
/// - Repository pattern implementation
/// - Custom search and filtering methods
/// - Pagination and sorting support
/// - MongoDB-specific query building
/// - Complex filter composition
/// </summary>
internal class DemoThingRepository : MongoDbBaseAggregateRepository<DemoThing>, IDemoThingRepository
{
    private readonly IMongoCollection<DemoThing> _demoThings;

    /// <summary>
    /// Initializes a new instance of DemoThingRepository with the database context.
    /// </summary>
    /// <param name="dbContext">The database context providing access to collections</param>
    public DemoThingRepository(MainDbContext dbContext)
        : base(dbContext.DemoThings)
    {
        _demoThings = dbContext.DemoThings;
    }

    /// <summary>
    /// Counts the total number of DemoThing entities matching the specified search criteria.
    /// This method is useful for implementing pagination and providing total count information
    /// to clients.
    /// 
    /// Example usage:
    /// var totalCount = await repository.CountAsync("search term", DemoThingType.DemoType1, 
    ///     DateTime.Today.AddDays(-30), DateTime.Today);
    /// </summary>
    /// <param name="term">Search term to filter by name, description, or userId</param>
    /// <param name="type">Optional filter by DemoThing type</param>
    /// <param name="fromDateTime">Optional start date for temporal filtering</param>
    /// <param name="toDateTime">Optional end date for temporal filtering</param>
    /// <returns>Total count of matching entities</returns>
    public async Task<long> CountAsync(string term, DemoThingType? type, DateTime? fromDateTime, DateTime? toDateTime)
    {
        var filter = GetFilter(term, type, fromDateTime, toDateTime);
        return await _demoThings.CountDocumentsAsync(filter);
    }

    /// <summary>
    /// Searches for DemoThing entities with advanced filtering, pagination, and sorting capabilities.
    /// This method demonstrates how to implement complex query operations that are common in
    /// real-world applications.
    /// 
    /// Example usage:
    /// var results = await repository.SearchAsync("search term", DemoThingType.DemoType1, 
    ///     1, 20, SortOrder.Ascending, DateTime.Today.AddDays(-30), DateTime.Today);
    /// </summary>
    /// <param name="term">Search term to filter by name, description, or userId</param>
    /// <param name="type">Optional filter by DemoThing type</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="recordsPerPage">Number of records per page</param>
    /// <param name="sortOrder">Sorting order for the results</param>
    /// <param name="fromDateTime">Optional start date for temporal filtering</param>
    /// <param name="toDateTime">Optional end date for temporal filtering</param>
    /// <returns>Collection of DemoThing entities matching the criteria</returns>
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

    /// <summary>
    /// Builds a MongoDB filter definition based on the provided search criteria.
    /// This private method demonstrates how to compose complex filters using MongoDB's
    /// filter builder pattern.
    /// 
    /// The method combines multiple filter conditions:
    /// - Type filtering (exact match)
    /// - Date range filtering (greater than or equal, less than or equal)
    /// - Text search using regex on name, description, and userId fields
    /// </summary>
    /// <param name="term">Search term for text-based filtering</param>
    /// <param name="type">Optional type filter</param>
    /// <param name="fromDateTime">Optional start date filter</param>
    /// <param name="toDateTime">Optional end date filter</param>
    /// <returns>MongoDB filter definition combining all search criteria</returns>
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
