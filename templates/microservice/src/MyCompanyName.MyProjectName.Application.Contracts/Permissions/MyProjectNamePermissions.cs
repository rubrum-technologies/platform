using Volo.Abp.Reflection;

namespace MyCompanyName.MyProjectName.Permissions;

public static class MyProjectNamePermissions
{
    public const string GroupName = "MyProjectName";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MyProjectNamePermissions));
    }
}
