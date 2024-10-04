using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class AppManager(IAppRepository repository) : DomainService
{
    public async Task<App> CreateAsync(
        Guid ownerId,
        string name,
        Version version,
        bool enabled,
        CancellationToken ct = default)
    {
        await CheckNameAsync(name, ct);

        return new App(GuidGenerator.Create(), CurrentTenant.Id, ownerId, name, version, enabled);
    }

    public async Task ChangeNameAsync(App app, string name, CancellationToken ct = default)
    {
        if (app.Name == name)
        {
            return;
        }

        await CheckNameAsync(name, ct);

        app.SetName(name);
    }

    private async Task CheckNameAsync(string name, CancellationToken ct)
    {
        if (await repository.AnyAsync(x => x.Name == name, ct))
        {
            throw new AppNameAlreadyExistsException(name);
        }
    }
}
