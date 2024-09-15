using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Owner", typeof(UserDefinition), typeof(OrganizationDefinition))]
public static partial class ResourceDefinition
{
    public static Permission Admin => Owner + Owner.Admin;
}
