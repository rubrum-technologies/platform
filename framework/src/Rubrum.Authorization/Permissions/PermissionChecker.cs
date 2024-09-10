using Volo.Abp.DependencyInjection;

namespace Rubrum.Authorization.Permissions;

public class PermissionChecker(
    IPermissionValueProviderManager permissionValueProviderManager) : IPermissionChecker, ITransientDependency
{
    public async Task<bool> IsGrantedAsync(
        Relationship relationship,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default)
    {
        var isGranted = false;

        foreach (var provider in permissionValueProviderManager.ValueProviders)
        {
            var result = await provider.GetResultAsync(relationship, context, ct);

            if (result == PermissionGrantResult.Granted)
            {
                isGranted = true;
            }
            else if (result == PermissionGrantResult.Prohibited)
            {
                return false;
            }
        }

        return isGranted;
    }
}
