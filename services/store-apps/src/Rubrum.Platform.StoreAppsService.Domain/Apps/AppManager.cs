using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class AppManager(
    IAppRepository repository,
    ICancellationTokenProvider cancellationTokenProvider) : DomainService
{
    public async Task<App> CreateAsync(
        string name,
        Version version,
        bool enabled)
    {
        await CheckNameAsync(name);

        return new App(GuidGenerator.Create(), CurrentTenant.Id, name, version, enabled);
    }

    public async Task ChangeNameAsync(App app, string name)
    {
        if (app.Name == name)
        {
            return;
        }

        await CheckNameAsync(name);

        app.SetName(name);
    }

    private async Task CheckNameAsync(string name)
    {
        var ct = cancellationTokenProvider.Token;

        if (await repository.AnyAsync(x => x.Name == name, ct))
        {
            throw new AppNameAlreadyExistsException(name);
        }
    }
}
