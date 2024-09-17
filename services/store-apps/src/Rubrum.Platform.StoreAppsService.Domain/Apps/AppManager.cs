using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class AppManager(
    IAppRepository repository,
    ICurrentTenant currentTenant,
    ICancellationTokenProvider ctProvider) : DomainService
{
    public async Task<App> CreateAsync(
        Guid? tenantId,
        string name,
        Version version,
        bool enabled)
    {
        using (currentTenant.Change(tenantId))
        {
            await CheckNameAsync(name);

            return new App(GuidGenerator.Create(), tenantId, name, version, enabled);
        }
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
        var cancellationToken = ctProvider.Token;

        if (await repository.AnyAsync(x => x.Name == name, cancellationToken))
        {
            throw new AppNameAlreadyExistsException(name);
        }
    }
}
