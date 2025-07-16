using CanBeYours.Application.Helpers;
using CodeBlock.DevKit.Administration.Domain.Permissions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CanBeYours.Infrastructure.DbContext;

internal static class DbSeeder
{
    public static void SeedPermissions(this IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var permissionRepository = serviceScope.ServiceProvider.GetService<IPermissionRepository>();

        var permissions = permissionRepository.GetListAsync().GetAwaiter().GetResult();

        foreach (var permission in Permissions.GetPermissions())
        {
            // Check if the permission already exists
            if (permissions.Any(p => p.SystemName == permission.SystemName))
                continue;

            var newPermission = Permission.Create(permissionRepository, permission.DisplayName, permission.SystemName, permission.GroupName);
            permissionRepository.Add(newPermission);
        }
    }
}
