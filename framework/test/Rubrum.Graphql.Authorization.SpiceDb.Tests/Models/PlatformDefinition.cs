using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation<UserDefinition.Ref>("Administrator")]
[Permission("SuperAdmin")]
public static partial class PlatformDefinition
{
    public static partial Permission SuperAdminConfigure() => Administrator;
}
