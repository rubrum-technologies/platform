using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.AdministrationService;

public class AdministrationServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        return Task.CompletedTask;
    }
}
