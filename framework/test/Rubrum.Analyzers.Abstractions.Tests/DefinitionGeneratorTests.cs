using Microsoft.CodeAnalysis;
using Rubrum.Analyzers;
using Rubrum.Authorization.Relations;
using Xunit;

namespace Rubrum.Authorization.Analyzers;

public class DefinitionGeneratorTests
{
    private readonly PortableExecutableReference[] _references =
    [
        MetadataReference.CreateFromFile(typeof(DefinitionAttribute).Assembly.Location),
    ];

    [Fact]
    public async Task GenerateSource_Document()
    {
        await SourceGeneratorTester.GetSnapshot<AuthorizationGenerator>(
            """
            using Rubrum.Authorization.Relations;

            namespace TestNamespace;

            [Definition]
            public static partial class UserDefinition
            {
            }


            [Definition]
            [Relation("Writer", typeof(UserDefinition))]
            [Relation("Reader", typeof(UserDefinition))]
            [Permission("Edit")]
            [Permission("View")]
            public static partial class DocumentDefinition
            {
                public static partial Permission EditConfigure() => Writer;

                public static partial Permission ViewConfigure() => Reader + Edit;
            }
            """,
            _references
        ).MatchMarkdownAsync();
    }

    [Fact]
    public async Task GenerateSource_SuperAdmin()
    {
        await SourceGeneratorTester.GetSnapshot<AuthorizationGenerator>(
            """
            using Rubrum.Authorization.Relations;

            namespace TestNamespace;

            [Definition]
            public static partial class UserDefinition
            {
            }

            [Definition]
            [Relation("Administrator", typeof(UserDefinition))]
            [Permission("SuperAdmin")]
            public static partial class PlatformDefinition
            {
                public static partial Permission SuperAdminConfigure() => Administrator;
            }


            [Definition]
            [Relation("Platform", typeof(PlatformDefinition))]
            [Permission("Admin")]
            public static partial class OrganizationDefinition
            {
                public static partial Permission AdminConfigure() => Platform.SuperAdmin;
            }

            [Definition]
            [Relation("Owner", typeof(UserDefinition), typeof(OrganizationDefinition))]
            [Permission("Admin")]
            public static partial class ResourceDefinition
            {
                public static partial Permission AdminConfigure() => Owner + Owner.Admin;
            }

            """,
            _references
        ).MatchMarkdownAsync();
    }

    [Fact]
    public async Task GenerateSource_Issue()
    {
        await SourceGeneratorTester.GetSnapshot<AuthorizationGenerator>(
            """
            using Rubrum.Authorization.Relations;

            namespace TestNamespace;

            [Definition]
            public static partial class UserDefinition
            {
            }

            [Definition]
            [Relation("Project", typeof(ProjectDefinition))]
            [Relation("Member", typeof(UserDefinition))]
            [Relation("BuiltInRole", typeof(ProjectDefinition))]
            [Permission("Delete")]
            [Permission("AddUser")]
            [Permission("AddPermission")]
            [Permission("RemovePermission")]
            public static partial class RoleDefinition
            {
                public static partial Permission DeleteConfigure() => Project.RoleManager - BuiltInRole.RoleManager;

                public static partial Permission AddUserConfigure() => Project.RoleManager;

                public static partial Permission AddPermissionConfigure() => Project.RoleManager - BuiltInRole.RoleManager;

                public static partial Permission RemovePermissionConfigure() => Project.RoleManager - BuiltInRole.RoleManager;
            }

            [Definition]
            [Relation("Issue", typeof(IssueDefinition))]
            [Permission("Delete")]
            public static partial class CommentDefinition
            {
                public static partial Permission DeleteConfigure()
                {
                    return null!;
                }
            }

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
            """,
            _references
        ).MatchMarkdownAsync();
    }
}
