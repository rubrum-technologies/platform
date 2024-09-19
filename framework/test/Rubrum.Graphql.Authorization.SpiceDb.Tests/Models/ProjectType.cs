using HotChocolate.Types;
using Rubrum.Graphql.Ddd;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Project>]
public static partial class ProjectType
{
    static partial void Configure(IObjectTypeDescriptor<Project> descriptor)
    {
        descriptor.Entity();

        descriptor
            .AddRelation(ProjectDefinition.IssueCreator)
            .AddRelation(ProjectDefinition.IssueAssigner)
            .AddRelation(ProjectDefinition.AnyIssueResolver)
            .AddRelation(ProjectDefinition.AssignedIssueResolver)
            .AddRelation(ProjectDefinition.CommentCreator)
            .AddRelation(ProjectDefinition.CommentDeleter)
            .AddRelation(ProjectDefinition.RoleManager)
            .AddPermission(ProjectDefinition.CreateIssue)
            .AddPermission(ProjectDefinition.CreateRole);
    }
}
