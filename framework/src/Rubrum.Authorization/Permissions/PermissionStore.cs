using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Rubrum.Authorization.Permissions;

public class PermissionStore(
    IDistributedCache<PermissionGrantCacheItem> cache,
    ICancellationTokenProvider cancellationTokenProvider) : IPermissionStore, ITransientDependency
{
    public async Task<bool> IsGrantedAsync(string name, string providerName, string providerKey)
    {
        var ct = cancellationTokenProvider.Token;

        var item = await cache.GetAsync(
            PermissionGrantCacheItem.CalculateCacheKey(name, providerName, providerKey),
            ct);

        return item?.IsGranted ?? false;
    }

    public async Task<MultiplePermissionGrantResult> IsGrantedAsync(
        string[] names,
        string providerName,
        string providerKey)
    {
        var ct = cancellationTokenProvider.Token;
        var result = new MultiplePermissionGrantResult();
        var items = new List<(string Name, PermissionGrantCacheItem? Item)>();

        foreach (var name in names)
        {
            var item = await cache.GetAsync(
                PermissionGrantCacheItem.CalculateCacheKey(name, providerName, providerKey),
                ct);

            items.Add((name, item));
        }

        foreach (var (name, item) in items)
        {
            result.Result.Add(name, GetGrantedResult(item?.IsGranted));
        }

        return result;
    }

    private static PermissionGrantResult GetGrantedResult(bool? isGranted)
    {
        if (isGranted is null)
        {
            return PermissionGrantResult.Undefined;
        }

        return isGranted.Value ? PermissionGrantResult.Granted : PermissionGrantResult.Prohibited;
    }
}
