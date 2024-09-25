using HotChocolate.Types;
using Rubrum.Graphql.Ddd;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Issue>]
public static partial class IssueType
{
    static partial void Configure(IObjectTypeDescriptor<Issue> descriptor)
    {
        descriptor.Entity();

        descriptor.BindDefinition(typeof(IssueDefinition));
    }
}
