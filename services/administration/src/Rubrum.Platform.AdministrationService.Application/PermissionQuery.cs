using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService;

[QueryType]
public static class PermissionQuery
{
    [Authorize]
    [GraphQLName("permissions")]
    public static Task<GetPermissionListResultDto> GetAsync(
        string providerName,
        string providerKey,
        [Service] IPermissionAppService service)
    {
        return service.GetAsync(providerName, providerKey);
    }
}
