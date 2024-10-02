using Volo.Abp;

namespace Rubrum.Authorization.Relations;

public sealed class PermissionExpression : Permission
{
    internal PermissionExpression(Permission left, PermissionOperator op, Permission right)
    {
        Left = Check.NotNull(left, nameof(left));
        Operator = Check.NotNull(op, nameof(op));
        Right = Check.NotNull(right, nameof(right));
    }

    public Permission Left { get; }

    public PermissionOperator Operator { get; }

    public Permission Right { get; }
}
