using Rubrum.Authorization.Relations;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Graphql.Relations;

public class NullRelationDirectiveBuilder : IRelationDirectiveBuilder, ITransientDependency
{
    public RelationDirective Build(Relation relation)
    {
        return new RelationDirective(string.Empty, string.Empty);
    }
}
