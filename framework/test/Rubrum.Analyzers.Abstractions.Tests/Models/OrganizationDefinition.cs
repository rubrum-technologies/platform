using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Platform", typeof(PlatformDefinition))]
[Permission("Admin")]
public static partial class OrganizationDefinition
{
    public static partial Permission AdminConfigure() => Platform.SuperAdmin;
}
