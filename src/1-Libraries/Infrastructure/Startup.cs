using CanBeYours.Application.Services.DemoThings;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure.DbContext;
using CanBeYours.Infrastructure.Mapping;
using CanBeYours.Infrastructure.Repositories;
using CodeBlock.DevKit.Infrastructure.Extensions;
using CodeBlock.DevKit.Infrastructure.Mapping;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CanBeYours.Infrastructure;

public static class Startup
{
    public static void AddInfrastructureModule(this IServiceCollection services)
    {
        services.RegisterHandlers(typeof(Startup));
        services.AddMongoDbContext();
        services.AddDomainServices();
        services.AddServices();
        services.AddMappingProfileFromAssemblyContaining<DemoThingMappingProfile>();
        services.RegisterBaseModule(typeof(Startup).Assembly);
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
    }

    public static void UseInfrastructureModule(this IServiceProvider serviceProvider)
    {
        serviceProvider.SeedSampleDemoThings();
    }

    private static void AddMongoDbContext(this IServiceCollection services)
    {
        services.AddScoped<MainDbContext>();
    }

    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDemoThingRepository, DemoThingRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDemoThingService, DemoThingService>();
    }
}
