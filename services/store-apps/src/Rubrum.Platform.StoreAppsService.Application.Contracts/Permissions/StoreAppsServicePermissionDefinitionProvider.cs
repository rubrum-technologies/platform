using Rubrum.Platform.StoreAppsService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rubrum.Platform.StoreAppsService.Permissions;

public class StoreAppsServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        context.AddGroup(StoreAppsServicePermissions.GroupName, L("Permission:StoreAppsService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StoreAppsServiceResource>(name);
    }
}
