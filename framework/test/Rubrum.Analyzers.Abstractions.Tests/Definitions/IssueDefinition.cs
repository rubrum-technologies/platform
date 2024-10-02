using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Definitions;

[Definition]
[Relation<ProjectDefinition.Ref>("Project")]
[Relation<UserDefinition.Ref>("Assigned")]
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
