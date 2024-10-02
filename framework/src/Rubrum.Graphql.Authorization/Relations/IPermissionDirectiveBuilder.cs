using Rubrum.Authorization.Relations;

namespace Rubrum.Graphql.Relations;

public interface IPermissionDirectiveBuilder
{
    PermissionDirective Build(PermissionLink link);
}
