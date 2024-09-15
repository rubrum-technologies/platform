namespace Rubrum.Authorization.Permissions;

public sealed class Permission
{
    public static Permission operator +(Permission a, Permission b) => new();

    public static Permission operator -(Permission a, Permission b) => new();

    public static Permission operator &(Permission a, Permission b) => new();
}
