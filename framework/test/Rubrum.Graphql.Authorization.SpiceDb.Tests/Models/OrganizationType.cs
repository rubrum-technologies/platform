using HotChocolate.Types;
using Rubrum.Graphql;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Organization>]
public static partial class OrganizationType
{
    static partial void Configure(IObjectTypeDescriptor<Organization> descriptor)
    {
        descriptor.Entity();

        descriptor.BindDefinition(typeof(OrganizationDefinition));
    }
}
