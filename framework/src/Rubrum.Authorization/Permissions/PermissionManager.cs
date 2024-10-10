using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Rubrum.Authorization.Permissions;

public class PermissionManager(IDistributedEventBus distributedEventBus) : IPermissionManager, ITransientDependency
{
    public async Task CreateOrUpdateAsync(
        IEnumerable<CreatePermissionGroupDefinitionEto> groups,
        CancellationToken ct = default)
    {
        foreach (var group in groups)
        {
            await distributedEventBus.PublishAsync(group, false, false);
        }
    }

    public async Task CreateOrUpdateAsync(
        IEnumerable<CreatePermissionDefinitionEto> permissions,
        CancellationToken ct = default)
    {
        foreach (var permission in permissions)
        {
            await distributedEventBus.PublishAsync(permission, false, false);
        }
    }

    public async Task SetAsync(GiveGrantPermissionEto grant, CancellationToken ct = default)
    {
        await distributedEventBus.PublishAsync(grant, false, false);
    }
}
