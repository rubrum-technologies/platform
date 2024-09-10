using Volo.Abp.Reflection;

namespace Rubrum.Platform.StoreAppsService.Permissions;

public static class StoreAppsServicePermissions
{
    public const string GroupName = "StoreAppsService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(StoreAppsServicePermissions));
    }
}
