using HotChocolate.Types;
using Rubrum.Graphql;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Role>]
public static partial class RoleType
{
    static partial void Configure(IObjectTypeDescriptor<Role> descriptor)
    {
        descriptor.Entity();

        descriptor.BindDefinition(typeof(RoleDefinition));
    }
}
