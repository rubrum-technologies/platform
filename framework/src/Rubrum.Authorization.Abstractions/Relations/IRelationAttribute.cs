using System.Collections.Immutable;

namespace Rubrum.Authorization.Relations;

public interface IRelationAttribute
{
    string Name { get; }

    ImmutableArray<DefinitionReference> Definitions { get; }
}
