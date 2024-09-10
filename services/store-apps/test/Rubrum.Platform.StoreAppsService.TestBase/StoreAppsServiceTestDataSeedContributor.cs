using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.StoreAppsService;

public class StoreAppsServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        return Task.CompletedTask;
    }
}
