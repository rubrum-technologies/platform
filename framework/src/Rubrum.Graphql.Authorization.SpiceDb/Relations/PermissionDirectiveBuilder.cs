using System.Text;
using Rubrum.Authorization.Relations;
using Rubrum.Graphql.Relations;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Graphql.SpiceDb.Relations;

public class PermissionDirectiveBuilder : IPermissionDirectiveBuilder, ITransientDependency
{
    public PermissionDirective Build(PermissionLink link)
    {
        return new PermissionDirective
        {
            Value = $"permission {link.Name} = {CreatePermissionValue(link.Config())}",
        };
    }

    private static StringBuilder CreatePermissionValue(Permission permission, StringBuilder? sb = null)
    {
        sb ??= new StringBuilder();

        if (permission is PermissionField field)
        {
            sb.Append(field.Name);
        }

        if (permission is PermissionExpression expression)
        {
            CreatePermissionValue(expression.Left, sb);
            sb.Append(expression.Operator switch
            {
                PermissionOperator.Arrow => "->",
                PermissionOperator.Exclusion => " - ",
                PermissionOperator.Intersection => " & ",
                PermissionOperator.Union => " + ",
                _ => string.Empty,
            });
            CreatePermissionValue(expression.Right, sb);
        }

        return sb;
    }
}
