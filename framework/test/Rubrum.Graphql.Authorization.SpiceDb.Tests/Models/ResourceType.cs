using HotChocolate.Types;
using Rubrum.Graphql.Ddd;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Resource>]
public static partial class ResourceType
{
    static partial void Configure(IObjectTypeDescriptor<Resource> descriptor)
    {
        descriptor.Entity();

        descriptor
            .AddRelation(ResourceDefinition.Owner)
            .AddPermission(ResourceDefinition.Admin);
    }
}
