using CanBeYours.Application.Dtos;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;

internal class SearchDemoThingsRequest : BaseQuery<SearchOutputDto<GetDemoThingDto>>
{
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

    public DemoThingType? Type { get; }
    public SortOrder SortOrder { get; }
    public string Term { get; }
    public int RecordsPerPage { get; }
    public int PageNumber { get; }
    public DateTime? FromDateTime { get; }
    public DateTime? ToDateTime { get; }
}
