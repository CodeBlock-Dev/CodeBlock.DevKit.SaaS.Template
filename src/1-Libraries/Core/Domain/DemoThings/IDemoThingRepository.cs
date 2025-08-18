using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;

namespace CanBeYours.Core.Domain.DemoThings;

/// <summary>
/// Repository interface for DemoThing domain entities. This interface demonstrates how to extend
/// the base repository with custom query methods specific to your business requirements.
/// 
/// This is an example implementation showing repository pattern usage. Replace with your actual
/// repository interfaces that define the data access contracts for your domain entities.
/// 
/// Key features demonstrated:
/// - Extending base repository interface for specific entity types
/// - Custom search and counting methods with filtering capabilities
/// - Pagination and sorting support
/// - Date range filtering for temporal queries
/// </summary>
public interface IDemoThingRepository : IBaseAggregateRepository<DemoThing>
{
    /// <summary>
    /// Counts the total number of DemoThing entities matching the specified search criteria.
    /// This method is useful for implementing pagination and providing total count information
    /// to clients.
    /// 
    /// Example usage:
    /// var totalCount = await repository.CountAsync("search term", DemoThingType.DemoType1, 
    ///     DateTime.Today.AddDays(-30), DateTime.Today);
    /// </summary>
    /// <param name="term">Search term to filter by name or description</param>
    /// <param name="type">Optional filter by DemoThing type</param>
    /// <param name="fromDateTime">Optional start date for temporal filtering</param>
    /// <param name="toDateTime">Optional end date for temporal filtering</param>
    /// <returns>Total count of matching entities</returns>
    Task<long> CountAsync(string term, DemoThingType? type, DateTime? fromDateTime, DateTime? toDateTime);
    
    /// <summary>
    /// Searches for DemoThing entities with advanced filtering, pagination, and sorting capabilities.
    /// This method demonstrates how to implement complex query operations that are common in
    /// real-world applications.
    /// 
    /// Example usage:
    /// var results = await repository.SearchAsync("search term", DemoThingType.DemoType1, 
    ///     1, 20, SortOrder.Ascending, DateTime.Today.AddDays(-30), DateTime.Today);
    /// </summary>
    /// <param name="term">Search term to filter by name or description</param>
    /// <param name="type">Optional filter by DemoThing type</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="recordsPerPage">Number of records per page</param>
    /// <param name="sortOrder">Sorting order for the results</param>
    /// <param name="fromDateTime">Optional start date for temporal filtering</param>
    /// <param name="toDateTime">Optional end date for temporal filtering</param>
    /// <returns>Collection of DemoThing entities matching the criteria</returns>
    Task<IEnumerable<DemoThing>> SearchAsync(
        string term,
        DemoThingType? type,
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        DateTime? fromDateTime,
        DateTime? toDateTime
    );
}
