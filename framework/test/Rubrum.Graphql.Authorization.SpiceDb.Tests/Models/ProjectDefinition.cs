using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("IssueCreator", typeof(RoleDefinition.MemberRelation))]
[Relation("IssueAssigner", typeof(RoleDefinition.MemberRelation))]
[Relation("AnyIssueResolver", typeof(RoleDefinition.MemberRelation))]
[Relation("AssignedIssueResolver", typeof(RoleDefinition.MemberRelation))]
[Relation("CommentCreator", typeof(RoleDefinition.MemberRelation))]
[Relation("CommentDeleter", typeof(RoleDefinition.MemberRelation))]
[Relation("RoleManager", typeof(RoleDefinition.MemberRelation))]
[Permission("CreateIssue")]
[Permission("CreateRole")]
public static partial class ProjectDefinition
{
    public static partial Permission CreateIssueConfigure() => IssueCreator;

    public static partial Permission CreateRoleConfigure() => RoleManager;
}
