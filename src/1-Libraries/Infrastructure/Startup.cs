using CanBeYours.Application;
using CanBeYours.Core.Domain.DemoThings;
using CanBeYours.Infrastructure.DbContext;
using CanBeYours.Infrastructure.Mapping;
using CanBeYours.Infrastructure.Repositories;
using CodeBlock.DevKit.Application.Extensions;
using CodeBlock.DevKit.Infrastructure.Mapping;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CanBeYours.Infrastructure;

public static class Startup
{
    public static void AddInfrastructureModule(this IServiceCollection services)
    {
        services.AddApplicationModule();
        services.RegisterHandlers(typeof(Startup));
        services.AddMongoDbContext();
        services.AddDomainServices();
        services.AddMappingProfileFromAssemblyContaining<DemoThingMappingProfile>();
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
    }

    public static void UseInfrastructureModule(this IServiceProvider serviceProvider)
    {
        serviceProvider.MigrateDatabes();
        serviceProvider.SeedPermissions();
    }

    public static void DropTestDatabase(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
        dbContext.DropTestDatabase();
    }

    private static void AddMongoDbContext(this IServiceCollection services)
    {
        services.AddScoped<MainDbContext>();
    }

    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDemoThingRepository, DemoThingRepository>();
    }
}
