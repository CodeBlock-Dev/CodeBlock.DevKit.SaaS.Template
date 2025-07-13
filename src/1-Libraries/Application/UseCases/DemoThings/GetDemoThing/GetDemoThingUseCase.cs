using CanBeYours.Application.Dtos;
using CanBeYours.Core.Domain.DemoThings;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CanBeYours.Application.UseCases.DemoThings.GetDemoThing;

internal class GetDemoThingUseCase : BaseQueryHandler, IRequestHandler<GetDemoThingRequest, Result<GetDemoThingDto>>
{
    private readonly IDemoThingRepository _demoThingRepository;

    public GetDemoThingUseCase(IDemoThingRepository demoThingRepository, IRequestDispatcher requestDispatcher, ILogger<GetDemoThingUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _demoThingRepository = demoThingRepository;
    }

    public async Task<Result<GetDemoThingDto>> Handle(GetDemoThingRequest request, CancellationToken cancellationToken)
    {
        var demoThing = await _demoThingRepository.GetByIdAsync(request.Id);
        if (demoThing == null)
            return Result<GetDemoThingDto>.NotFound();

        return Result<GetDemoThingDto>.Success(MapToDto(demoThing));
    }

    private static GetDemoThingDto MapToDto(DemoThing demoThing)
    {
        return new GetDemoThingDto
        {
            Id = demoThing.Id,
            Name = demoThing.Name,
            Description = demoThing.Description,
            CreationTime = demoThing.CreationTime
        };
    }
} 