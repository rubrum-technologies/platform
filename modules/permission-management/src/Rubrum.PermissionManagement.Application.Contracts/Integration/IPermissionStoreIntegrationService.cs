using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;

namespace Rubrum.PermissionManagement.Integration;

[IntegrationService]
public interface IPermissionStoreIntegrationService : IApplicationService
{
    Task<IReadOnlyDictionary<string, PermissionGrantResult>> IsGrantedAsync(PermissionGrantInput input);
}
