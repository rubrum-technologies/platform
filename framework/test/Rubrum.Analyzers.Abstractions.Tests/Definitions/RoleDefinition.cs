using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Definitions;

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
