using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;

namespace Rubrum.PermissionManagement.Integration;

public class PermissionStoreIntegrationService(
    IPermissionStore store) : ApplicationService, IPermissionStoreIntegrationService
{
    public async Task<IReadOnlyDictionary<string, PermissionGrantResult>> IsGrantedAsync(PermissionGrantInput input)
    {
        var result = await store.IsGrantedAsync(input.Names, input.ProviderName, input.ProviderKey);

        return result.Result;
    }
}
