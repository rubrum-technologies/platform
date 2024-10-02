using Microsoft.CodeAnalysis;
using Rubrum.Analyzers;
using Rubrum.Authorization.Relations;
using Shouldly;
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
            [Relation<UserDefinition.Ref>("Writer")]
            [Relation<UserDefinition.Ref>("Reader")]
            [Permission("Edit")]
            [Permission("View")]
            public static partial class DocumentDefinition
            {
                public static partial Permission EditConfigure() => Writer;

                public static partial Permission ViewConfigure() => Reader + Edit;
            }
            """,
            _references).ShouldNotBeNull().MatchMarkdownAsync();
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
            [Relation<UserDefinition.Ref>("Administrator")]
            [Permission("SuperAdmin")]
            public static partial class PlatformDefinition
            {
                public static partial Permission SuperAdminConfigure() => Administrator;
            }


            [Definition]
            [Relation<PlatformDefinition.Ref>("Platform")]
            [Permission("Admin")]
            public static partial class OrganizationDefinition
            {
                public static partial Permission AdminConfigure() => Platform.SuperAdmin;
            }

            [Definition]
            [Relation<UserDefinition.Ref, OrganizationDefinition.Ref>("Owner")]
            [Permission("Admin")]
            public static partial class ResourceDefinition
            {
                public static partial Permission AdminConfigure() => Owner + Owner.Admin;
            }

            """,
            _references).ShouldNotBeNull().MatchMarkdownAsync();
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
            [Relation<ProjectDefinition.Ref>("Project")]
            [Relation<UserDefinition.Ref>("Member")]
            [Relation<ProjectDefinition.Ref>("BuiltInRole")]
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
            [Relation<IssueDefinition.Ref>("Issue")]
            public static partial class CommentDefinition
            {
            }

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
            """,
            _references).ShouldNotBeNull().MatchMarkdownAsync();
    }
}
