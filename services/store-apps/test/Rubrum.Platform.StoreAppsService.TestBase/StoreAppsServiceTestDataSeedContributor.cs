using Rubrum.Platform.StoreAppsService.Apps;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using static Rubrum.Platform.StoreAppsService.AppTestConstants;

namespace Rubrum.Platform.StoreAppsService;

public class StoreAppsServiceTestDataSeedContributor(
    IAppRepository repository,
    AppManager manager) : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await repository.GetCountAsync() > 0)
        {
            return;
        }

        await repository.InsertAsync(
            await manager.CreateAsync(TestOwnerId, TestName, TestVersion, true),
            true);
    }
}
