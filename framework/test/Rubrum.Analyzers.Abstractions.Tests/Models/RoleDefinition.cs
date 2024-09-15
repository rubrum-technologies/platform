using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Project", typeof(ProjectDefinition))]
[Relation("Member", typeof(UserDefinition))]
[Relation("BuiltInRole", typeof(ProjectDefinition))]
public static partial class RoleDefinition
{
    public static Permission Delete => Project.RoleManager - BuiltInRole.RoleManager;

    public static Permission AddUser => Project.RoleManager;

    public static Permission AddPermission => Project.RoleManager - BuiltInRole.RoleManager;

    public static Permission RemovePermission => Project.RoleManager - BuiltInRole.RoleManager;
}
