namespace Rubrum.Authorization.Permissions;

[Serializable]
public class PermissionGrantCacheItem
{
    public const string CacheKeyFormat = "pn:{0},pk:{1},n:{2}";

    public PermissionGrantCacheItem()
    {
    }

    public PermissionGrantCacheItem(bool isGranted)
    {
        IsGranted = isGranted;
    }

    public bool IsGranted { get; set; }

    public static string CalculateCacheKey(string name, string providerName, string providerKey)
    {
        return string.Format(CacheKeyFormat, providerName, providerKey, name);
    }
}
