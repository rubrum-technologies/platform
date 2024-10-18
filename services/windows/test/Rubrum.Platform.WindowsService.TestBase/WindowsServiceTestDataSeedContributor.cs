using Rubrum.Platform.WindowsService.Windows;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Rubrum.Platform.WindowsService;

public class WindowsServiceTestDataSeedContributor(
    IUnitOfWorkManager unitOfWorkManager,
    IWindowRepository repository) : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        throw new NotImplementedException();
    }
}
