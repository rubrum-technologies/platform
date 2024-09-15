using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

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
