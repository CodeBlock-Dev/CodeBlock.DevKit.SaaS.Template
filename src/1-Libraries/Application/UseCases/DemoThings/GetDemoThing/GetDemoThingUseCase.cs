using AutoMapper;
using CanBeYours.Application.Dtos;
using CanBeYours.Application.Exceptions;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.GetDemoThing;

internal class GetDemoThingUseCase : BaseQueryHandler, IRequestHandler<GetDemoThingRequest, GetDemoThingDto>
{
    private readonly IDemoThingRepository _demoThingRepository;

    public GetDemoThingUseCase(IDemoThingRepository demoThingRepository, IMapper mapper, ILogger<GetDemoThingUseCase> logger)
        : base(mapper, logger)
    {
        _demoThingRepository = demoThingRepository;
    }

    public async Task<GetDemoThingDto> Handle(GetDemoThingRequest request, CancellationToken cancellationToken)
    {
        var demoThing = await _demoThingRepository.GetByIdAsync(request.Id);
        if (demoThing == null)
            throw DemoThingApplicationExceptions.DemoThingNotFound(request.Id);

        return _mapper.Map<GetDemoThingDto>(demoThing);
    }
}
