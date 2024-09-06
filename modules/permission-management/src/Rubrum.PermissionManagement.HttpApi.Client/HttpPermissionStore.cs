using Rubrum.PermissionManagement.Integration;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;

namespace Rubrum.PermissionManagement;

[Dependency(ReplaceServices = true)]
public class HttpPermissionStore(IPermissionStoreIntegrationService httpStore) : IPermissionStore, ITransientDependency
{
    public async Task<bool> IsGrantedAsync(string name, string providerName, string providerKey)
    {
        var granted = await GetGrantedAsync([name], providerName, providerKey);
        var result = granted.FirstOrDefault().Value;

        return result == PermissionGrantResult.Granted;
    }

    public async Task<MultiplePermissionGrantResult> IsGrantedAsync(
        string[] names,
        string providerName,
        string providerKey)
    {
        var granted = await GetGrantedAsync(names, providerName, providerKey);
        var result = new MultiplePermissionGrantResult();

        foreach (var (key, value) in granted)
        {
            result.Result.Add(key, value);
        }

        return result;
    }

    private Task<IReadOnlyDictionary<string, PermissionGrantResult>> GetGrantedAsync(
        string[] names,
        string providerName,
        string providerKey)
    {
        return httpStore.IsGrantedAsync(new PermissionGrantInput
        {
            Names = names,
            ProviderName = providerName,
            ProviderKey = providerKey,
        });
    }
}
