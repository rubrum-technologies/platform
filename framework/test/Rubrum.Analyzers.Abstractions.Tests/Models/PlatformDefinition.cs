using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Administrator", typeof(UserDefinition))]
[Permission("SuperAdmin")]
public static partial class PlatformDefinition
{
    public static partial Permission SuperAdminConfigure() => Administrator;
}
