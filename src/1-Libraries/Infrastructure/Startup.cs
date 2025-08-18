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

/// <summary>
/// Infrastructure module startup class that demonstrates how to configure and register all infrastructure services.
/// This class serves as a learning example showing how to set up dependency injection, database context,
/// repositories, mapping profiles, and other infrastructure components.
/// 
/// IMPORTANT: This is an example implementation for learning purposes. Replace the DemoThing-related services
/// with your actual business domain services and repositories.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers all infrastructure services with the dependency injection container.
    /// This method demonstrates the proper order of service registration and how to integrate
    /// different modules together.
    /// 
    /// Example usage in Program.cs:
    /// builder.Services.AddInfrastructureModule();
    /// </summary>
    /// <param name="services">The service collection to register services with</param>
    public static void AddInfrastructureModule(this IServiceCollection services)
    {
        services.AddApplicationModule();
        services.RegisterHandlers(typeof(Startup));
        services.AddMongoDbContext();
        services.AddDomainServices();
        services.AddMappingProfileFromAssemblyContaining<DemoThingMappingProfile>();
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
    }

    /// <summary>
    /// Initializes the infrastructure module after the service provider is built.
    /// This method runs database migrations and seeds initial data like permissions.
    /// 
    /// Example usage in Program.cs:
    /// app.UseInfrastructureModule();
    /// </summary>
    /// <param name="serviceProvider">The built service provider</param>
    public static void UseInfrastructureModule(this IServiceProvider serviceProvider)
    {
        serviceProvider.MigrateDatabes();
        serviceProvider.SeedPermissions();
    }

    /// <summary>
    /// Safely drops test databases for testing purposes.
    /// Only drops databases that start with "Test_" prefix to prevent accidental
    /// deletion of production databases.
    /// 
    /// Example usage in test cleanup:
    /// serviceProvider.DropTestDatabase();
    /// </summary>
    /// <param name="serviceProvider">The service provider containing the database context</param>
    public static void DropTestDatabase(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<MainDbContext>();
        dbContext.DropTestDatabase();
    }

    /// <summary>
    /// Registers the MongoDB database context as a scoped service.
    /// This method demonstrates how to register database contexts with proper lifetime management.
    /// </summary>
    /// <param name="services">The service collection to register services with</param>
    private static void AddMongoDbContext(this IServiceCollection services)
    {
        services.AddScoped<MainDbContext>();
    }

    /// <summary>
    /// Registers domain-specific services and repositories.
    /// This method shows how to register your business domain repositories and services.
    /// Replace DemoThingRepository with your actual repository implementations.
    /// </summary>
    /// <param name="services">The service collection to register services with</param>
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IDemoThingRepository, DemoThingRepository>();
    }
}
