namespace Rubrum.Authorization.Permissions;

internal sealed class PermissionExpression(Permission left, PermissionOperator op, Permission right) : Permission
{
    public Permission Left => left;

    public PermissionOperator Operator => op;

    public Permission Right => right;
}
