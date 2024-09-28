using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        return Task.CompletedTask;
    }
}
