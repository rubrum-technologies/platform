using Rubrum.Authorization.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation<UserDefinition.Ref, OrganizationDefinition.Ref>("Owner")]
[Permission("Admin")]
public static partial class ResourceDefinition
{
    public static partial Permission AdminConfigure() => Owner + Owner.Admin;
}
