namespace Rubrum.Platform.DataSourceService.Permissions;

public static class DataSourceServicePermissions
{
    public const string GroupName = "DataSourceService";

    public static class DataSources
    {
        public const string Default = GroupName + ".DataSources";
        public const string Create = Default + ".Create";
    }
}
