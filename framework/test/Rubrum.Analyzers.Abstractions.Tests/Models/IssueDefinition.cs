using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Project", typeof(ProjectDefinition))]
[Relation("Assigned", typeof(UserDefinition))]
[Permission("Assign")]
[Permission("Resolve")]
[Permission("CreateComment")]
[Permission("ProjectCommentDeleter")]
public static partial class IssueDefinition
{
    public static partial Permission AssignConfigure() => Project.IssueAssigner;

    public static partial Permission ResolveConfigure() =>
        (Project.AssignedIssueResolver & Assign) + Project.AnyIssueResolver;

    public static partial Permission CreateCommentConfigure() => Project.CommentCreator;

    public static partial Permission ProjectCommentDeleterConfigure() => Project.CommentDeleter;
}
