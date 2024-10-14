using Rubrum.Platform.StoreAppsService.Apps;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using static Rubrum.Platform.StoreAppsService.AppTestConstants;

namespace Rubrum.Platform.StoreAppsService;

public class StoreAppsServiceTestDataSeedContributor(
    IUnitOfWorkManager unitOfWorkManager,
    IAppRepository repository) : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        using var uow = unitOfWorkManager.Begin(true, true);

        if (await repository.GetCountAsync() > 0)
        {
            return;
        }

        var app = new App(TestAppId, null, TestOwnerId, TestName, TestVersion, true);
        await repository.InsertAsync(app, true);

        var app2 = new App(TestAppId2, null, TestOwnerId, "Второе приложение dataseed", TestVersion, false);
        await repository.InsertAsync(app2, true);

        await uow.CompleteAsync();
    }
}
