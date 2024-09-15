using Rubrum.Authorization.Permissions;

namespace Rubrum.Authorization.Analyzers.Models;

[Definition]
[Relation("Administrator", typeof(UserDefinition))]
public static partial class PlatformDefinition
{
    public static Permission SuperAdmin => Administrator;
}
