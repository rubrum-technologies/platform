using Rubrum.Platform.AdministrationService.EntityFrameworkCore;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EntityFrameworkCore.Migrations;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Rubrum.Platform.AdministrationService.DbMigrations;

public class EfCoreRuntimeDatabaseMigrator(
    IUnitOfWorkManager unitOfWorkManager,
    IServiceProvider serviceProvider,
    ICurrentTenant currentTenant,
    IAbpDistributedLock abpDistributedLock,
    IDistributedEventBus distributedEventBus,
    IPermissionDefinitionManager permissionDefinitionManager,
    IPermissionDataSeeder permissionDataSeeder,
    ILoggerFactory loggerFactory)
    : EfCoreRuntimeDatabaseMigratorBase<AdministrationServiceDbContext>(
        AdministrationServiceDbProperties.ConnectionStringName,
        unitOfWorkManager,
        serviceProvider,
        currentTenant,
        abpDistributedLock,
        distributedEventBus,
        loggerFactory)
{
    protected override async Task SeedAsync()
    {
        await ServiceProvider.GetRequiredService<IDataSeeder>().SeedAsync();

        using var uow = UnitOfWorkManager.Begin(true, true);

        var permissionNames = (await permissionDefinitionManager.GetPermissionsAsync())
            .Where(p => p.MultiTenancySide.HasFlag(MultiTenancySides.Host))
            .Where(p => p.Providers.Count == 0 ||
                p.Providers.Contains(RolePermissionValueProvider.ProviderName))
            .Select(p => p.Name)
            .ToArray();

        await permissionDataSeeder.SeedAsync(
            RolePermissionValueProvider.ProviderName,
            "admin",
            permissionNames);

        await uow.CompleteAsync();
    }
}
