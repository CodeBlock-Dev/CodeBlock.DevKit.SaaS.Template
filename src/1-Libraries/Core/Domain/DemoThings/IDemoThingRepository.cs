using CodeBlock.DevKit.Domain.Services;

namespace CanBeYours.Core.Domain.DemoThings;

public interface IDemoThingRepository : IBaseAggregateRepository<DemoThing>
{
    Task<long> CountAsync(string term);
    Task<IEnumerable<DemoThing>> SearchAsync(string term, int pageNumber, int recordsPerPage);
}
