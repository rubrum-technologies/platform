using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.BlobStorageService;

public class BlobStorageServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        return Task.CompletedTask;
    }
}
