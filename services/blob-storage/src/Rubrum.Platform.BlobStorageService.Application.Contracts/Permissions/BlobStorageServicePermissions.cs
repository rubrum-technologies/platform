using Volo.Abp.Reflection;

namespace Rubrum.Platform.BlobStorageService.Permissions;

public static class BlobStorageServicePermissions
{
    public const string GroupName = "BlobStorageService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BlobStorageServicePermissions));
    }

    public static class Blobs
    {
        public const string Default = GroupName + ".Blobs";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}
