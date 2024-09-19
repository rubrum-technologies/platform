using HotChocolate.Types;
using Rubrum.Graphql.Ddd;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Role>]
public static partial class RoleType
{
    static partial void Configure(IObjectTypeDescriptor<Role> descriptor)
    {
        descriptor.Entity();

        descriptor
            .AddRelation(RoleDefinition.Project)
            .AddRelation(RoleDefinition.Member)
            .AddRelation(RoleDefinition.BuiltInRole)
            .AddPermission(RoleDefinition.Delete)
            .AddPermission(RoleDefinition.AddPermission)
            .AddPermission(RoleDefinition.AddUser)
            .AddPermission(RoleDefinition.RemovePermission);
    }
}
