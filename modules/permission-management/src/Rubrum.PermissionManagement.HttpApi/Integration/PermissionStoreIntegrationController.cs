using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization.Permissions;

namespace Rubrum.PermissionManagement.Integration;

[RemoteService(Name = RubrumPermissionManagementRemoteServiceConstants.RemoteServiceName)]
[Area(RubrumPermissionManagementRemoteServiceConstants.ModuleName)]
[ControllerName("PermissionStoreIntegration")]
[Route("integration-api/permission-management/rubrum/permissions/is-granted")]
public class PermissionStoreIntegrationController(IPermissionStoreIntegrationService service)
    : AbpControllerBase, IPermissionStoreIntegrationService
{
    [HttpGet]
    public async Task<IReadOnlyDictionary<string, PermissionGrantResult>> IsGrantedAsync(
        [FromQuery] PermissionGrantInput input)
    {
        return await service.IsGrantedAsync(input);
    }
}
