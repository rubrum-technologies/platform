using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.SimpleStateChecking;
using Volo.Abp.Threading;

namespace Rubrum.Authorization.Permissions;

public class PermissionStateChecker : ISimpleStateChecker<PermissionDefinition>, ITransientDependency
{
    public async Task<bool> IsEnabledAsync(SimpleStateCheckerContext<PermissionDefinition> context)
    {
        var cache = context.ServiceProvider.GetRequiredService<IDistributedCache<PermissionCacheItem>>();
        var cancellationTokenProvider = context.ServiceProvider.GetRequiredService<ICancellationTokenProvider>();
        var ct = cancellationTokenProvider.Token;

        var item = await cache.GetAsync(PermissionCacheItem.CalculateCacheKey(context.State.Name), ct);

        return item?.IsEnable ?? false;
    }
}
