using CodeBlock.DevKit.Core.Helpers;
using CodeBlock.DevKit.Domain.Services;

namespace CanBeYours.Core.Domain.DemoThings;

public interface IDemoThingRepository : IBaseAggregateRepository<DemoThing>
{
    Task<long> CountAsync(string term, DemoThingType? type, DateTime? fromDateTime, DateTime? toDateTime);
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
