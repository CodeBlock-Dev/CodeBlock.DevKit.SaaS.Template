using CanBeYours.Application.Dtos.DemoThings;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;

/// <summary>
/// Query request for searching DemoThing entities with various filtering and pagination options.
/// This class demonstrates how to implement search query requests that include:
/// - Multiple search criteria (term, type, date range)
/// - Pagination parameters (page number, records per page)
/// - Sorting options
/// - Flexible filtering capabilities
/// The DemoThing functionality shown here is just an example to help you learn how to implement
/// your own unique features into the current codebase.
/// </summary>
internal class SearchDemoThingsRequest : BaseQuery<SearchOutputDto<GetDemoThingDto>>
{
    /// <summary>
    /// Initializes a new instance of the SearchDemoThingsRequest with comprehensive search parameters.
    /// </summary>
    /// <param name="term">The search term for filtering by name, description, or user information</param>
    /// <param name="type">Optional filter by demo thing type</param>
    /// <param name="pageNumber">The page number for pagination (1-based)</param>
    /// <param name="recordsPerPage">The number of records per page</param>
    /// <param name="sortOrder">The sort order for the results</param>
    /// <param name="fromDateTime">Optional start date for filtering by creation date</param>
    /// <param name="toDateTime">Optional end date for filtering by creation date</param>
    /// <param name="options">Optional query options for customizing the search behavior</param>
    public SearchDemoThingsRequest(
        string term,
        DemoThingType? type,
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        DateTime? fromDateTime,
        DateTime? toDateTime,
        QueryOptions options = null
    )
        : base(options)
    {
        Term = term;
        Type = type;
        RecordsPerPage = recordsPerPage;
        PageNumber = pageNumber;
        SortOrder = sortOrder;
        FromDateTime = fromDateTime;
        ToDateTime = toDateTime;
    }

    /// <summary>
    /// Optional filter by demo thing type. When null, all types are included in the search.
    /// </summary>
    public DemoThingType? Type { get; }

    /// <summary>
    /// The sort order for the search results.
    /// </summary>
    public SortOrder SortOrder { get; }

    /// <summary>
    /// The search term for filtering by name, description, or user information.
    /// </summary>
    public string Term { get; }

    /// <summary>
    /// The number of records to return per page.
    /// </summary>
    public int RecordsPerPage { get; }

    /// <summary>
    /// The page number for pagination (1-based).
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Optional start date for filtering by creation date.
    /// </summary>
    public DateTime? FromDateTime { get; }

    /// <summary>
    /// Optional end date for filtering by creation date.
    /// </summary>
    public DateTime? ToDateTime { get; }
}
