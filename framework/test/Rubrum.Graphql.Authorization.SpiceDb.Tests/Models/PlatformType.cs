using HotChocolate.Types;
using Rubrum.Graphql;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Platform>]
public static partial class PlatformType
{
    static partial void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Entity();

        descriptor.BindDefinition(typeof(PlatformDefinition));
    }
}
