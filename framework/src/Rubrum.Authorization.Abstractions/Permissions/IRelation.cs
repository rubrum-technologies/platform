using System.Collections.Immutable;

namespace Rubrum.Authorization.Permissions;

public interface IRelation
{
    string Name { get; }

    ImmutableArray<Type> Definitions { get; }
}
