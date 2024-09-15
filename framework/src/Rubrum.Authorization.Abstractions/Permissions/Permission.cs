#pragma warning disable S4050

namespace Rubrum.Authorization.Permissions;

public abstract class Permission
{
    private protected Permission()
    {
    }

    public static Permission operator +(Permission a, Permission b) =>
        new PermissionExpression(a, PermissionOperator.Union, b);

    public static Permission operator -(Permission a, Permission b) =>
        new PermissionExpression(a, PermissionOperator.Exclusion, b);

    public static Permission operator &(Permission a, Permission b) =>
        new PermissionExpression(a, PermissionOperator.Intersection, b);
}
