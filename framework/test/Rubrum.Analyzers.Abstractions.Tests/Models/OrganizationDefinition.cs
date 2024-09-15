using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Platform", typeof(PlatformDefinition))]
public static partial class OrganizationDefinition
{
    public static Permission Admin => Platform.SuperAdmin;
}
