using Rubrum.Authorization.Relations;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Graphql.Relations;

public class NullPermissionDirectiveBuilder : IPermissionDirectiveBuilder, ITransientDependency
{
    public PermissionDirective Build(PermissionLink link)
    {
        return new PermissionDirective(string.Empty, string.Empty);
    }
}
