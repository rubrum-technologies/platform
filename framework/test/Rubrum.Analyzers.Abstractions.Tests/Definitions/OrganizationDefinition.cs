using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Definitions;

[Definition]
[Relation<PlatformDefinition.Ref>("Platform")]
[Permission("Admin")]
public static partial class OrganizationDefinition
{
    public static partial Permission AdminConfigure() => Platform.SuperAdmin;
}
