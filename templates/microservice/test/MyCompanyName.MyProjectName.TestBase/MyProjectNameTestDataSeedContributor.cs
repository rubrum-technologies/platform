using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.MyProjectName;

public class MyProjectNameTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        return Task.CompletedTask;
    }
}
