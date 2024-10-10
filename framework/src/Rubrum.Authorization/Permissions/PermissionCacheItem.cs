namespace Rubrum.Authorization.Permissions;

public class PermissionCacheItem
{
    public const string CacheKeyFormat = "permission:{0}";

    public bool IsEnable { get; set; }

    public static string CalculateCacheKey(string name)
    {
        return string.Format(CacheKeyFormat, name);
    }
}
