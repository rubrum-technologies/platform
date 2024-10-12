using Rubrum.Platform.StoreAppsService.Apps;
using Testcontainers.PostgreSql;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using static Rubrum.Platform.StoreAppsService.AppTestConstants;

namespace Rubrum.Platform.StoreAppsService;

public class StoreAppsServiceTestDataSeedContributor(
    PostgreSqlContainer postgreSqlContainer,
    IUnitOfWorkManager unitOfWorkManager,
    IAppRepository repository,
    AppManager manager) : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        if (await repository.GetCountAsync() > 0)
        {
            return;
        }

        await repository.InsertAsync(
            await manager.CreateAsync(TestOwnerId, TestName, TestVersion, true),
            true);

        await uow.CompleteAsync();
    }
}
