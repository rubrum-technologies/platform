using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Definitions;

[Definition]
[Relation<RoleDefinition.Ref.Member>("IssueCreator")]
[Relation<RoleDefinition.Ref.Member>("IssueAssigner")]
[Relation<RoleDefinition.Ref.Member>("AnyIssueResolver")]
[Relation<RoleDefinition.Ref.Member>("AssignedIssueResolver")]
[Relation<RoleDefinition.Ref.Member>("CommentCreator")]
[Relation<RoleDefinition.Ref.Member>("CommentDeleter")]
[Relation<RoleDefinition.Ref.Member>("RoleManager")]
[Permission("CreateIssue")]
[Permission("CreateRole")]
public static partial class ProjectDefinition
{
    public static partial Permission CreateIssueConfigure() => IssueCreator;

    public static partial Permission CreateRoleConfigure() => RoleManager;
}
