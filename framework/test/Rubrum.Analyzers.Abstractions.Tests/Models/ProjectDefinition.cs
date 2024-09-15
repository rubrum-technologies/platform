using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("IssueCreator", typeof(RoleDefinition.MemberRelation))]
[Relation("IssueAssigner", typeof(RoleDefinition.MemberRelation))]
[Relation("AnyIssueResolver", typeof(RoleDefinition.MemberRelation))]
[Relation("AssignedIssueResolver", typeof(RoleDefinition.MemberRelation))]
[Relation("CommentCreator", typeof(RoleDefinition.MemberRelation))]
[Relation("CommentDeleter", typeof(RoleDefinition.MemberRelation))]
[Relation("RoleManager", typeof(RoleDefinition.MemberRelation))]
public static partial class ProjectDefinition
{
    public static Permission CreateIssue => IssueCreator;

    public static Permission CreateRole => RoleManager;
}
