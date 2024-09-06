using Rubrum.Platform.BlobStorageService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Rubrum.Platform.BlobStorageService.Permissions;

public class BlobStorageServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(BlobStorageServicePermissions.GroupName, L("Permission:BlobStorageService"));

        var blobs = group.AddPermission(
            BlobStorageServicePermissions.Blobs.Default,
            L("Permission:BlobManagement"));
        blobs.AddChild(BlobStorageServicePermissions.Blobs.Create, L("Permission:Create"));
        blobs.AddChild(BlobStorageServicePermissions.Blobs.Update, L("Permission:Update"));
        blobs.AddChild(BlobStorageServicePermissions.Blobs.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BlobStorageServiceResource>(name);
    }
}
