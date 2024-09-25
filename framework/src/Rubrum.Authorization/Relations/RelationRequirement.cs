using Microsoft.AspNetCore.Authorization;

namespace Rubrum.Authorization.Relations;

public class RelationRequirement : IAuthorizationRequirement
{
    public PermissionLink Permission { get; init; } = default!;

    public ResourceReference Resource { get; init; } = default!;

    public SubjectReference? Subject { get; init; }
}
