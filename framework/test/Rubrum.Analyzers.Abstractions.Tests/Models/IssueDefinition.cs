using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Project", typeof(ProjectDefinition))]
[Relation("Assigned", typeof(UserDefinition))]
public static partial class IssueDefinition
{
    public static Permission Assign => Project.IssueAssigner;

    public static Permission Resolve => (Project.AssignedIssueResolver & Assign) + Project.AnyIssueResolver;

    public static Permission CreateComment => Project.CommentCreator;

    public static Permission ProjectCommentDeleter => Project.CommentDeleter;
}
