using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.Threading;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class AppManager(
    IGuidGenerator guidGenerator,
    IAppRepository appRepository,
    ICancellationTokenProvider cancellationTokenProvider) : DomainService
{
    public async Task<App> CreateAsync(
        string name,
        string version,
        bool enabled)
    {
        await CheckNameAsync(name);

        return new App(guidGenerator.Create(), name, version, enabled);
    }

    private async Task CheckNameAsync(string name)
    {
        var cancellationToken = cancellationTokenProvider.Token;

        if (await appRepository.AnyAsync(x => x.Name == name, cancellationToken))
        {
            throw new AppNameAlreadyExistsException(name);
        }
    }
}
