using Rubrum.Platform.AdministrationService.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EntityFrameworkCore.Migrations;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Rubrum.Platform.AdministrationService.DbMigrations;

public class EfCoreRuntimeDatabaseMigrator(
    IUnitOfWorkManager unitOfWorkManager,
    IServiceProvider serviceProvider,
    ICurrentTenant currentTenant,
    IAbpDistributedLock abpDistributedLock,
    IDistributedEventBus distributedEventBus,
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
    }
}
