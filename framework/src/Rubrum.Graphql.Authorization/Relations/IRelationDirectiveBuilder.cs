using Rubrum.Authorization.Relations;

namespace Rubrum.Graphql.Relations;

public interface IRelationDirectiveBuilder
{
    RelationDirective Build(Relation relation);
}
