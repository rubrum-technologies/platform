using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Owner", typeof(UserDefinition), typeof(OrganizationDefinition))]
[Permission("Admin")]
public static partial class ResourceDefinition
{
    public static partial Permission AdminConfigure() => Owner + Owner.Admin;
}
