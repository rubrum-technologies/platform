using System.Collections.Immutable;

namespace Rubrum.Authorization.Relations;

public interface IRelation
{
    string Name { get; }

    ImmutableArray<Type> Definitions { get; }
}
