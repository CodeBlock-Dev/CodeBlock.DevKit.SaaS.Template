using CanBeYours.Application.Dtos;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;

namespace CanBeYours.Application.UseCases.DemoThings.SearchDemoThings;

internal class SearchDemoThingsRequest : BaseQuery<SearchOutputDto<GetDemoThingDto>>
{
    public SearchDemoThingsRequest(string term, int pageNumber, int recordsPerPage, SortOrder sortOrder)
    {
        Term = term;
        PageNumber = pageNumber;
        RecordsPerPage = recordsPerPage;
        SortOrder = sortOrder;
    }

    public string Term { get; }
    public int PageNumber { get; }
    public int RecordsPerPage { get; }
    public SortOrder SortOrder { get; }
} 