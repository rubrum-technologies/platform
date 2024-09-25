using Microsoft.AspNetCore.Authorization;

namespace Rubrum.Authorization.Relations;

public class RelationRequirementHandler(
    IRelationStore relationStore) : AuthorizationHandler<RelationRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RelationRequirement requirement)
    {
        var permission = requirement.Permission;
        var resource = requirement.Resource;
        var subject = requirement.Subject;
        bool result;

        if (subject is not null)
        {
            result = await relationStore.IsGrantedAsync(
                permission,
                resource,
                subject);
        }
        else
        {
            result = await relationStore.IsGrantedWithUserAsync(
                permission,
                resource);
        }

        if (result)
        {
            context.Succeed(requirement);
        }
    }
}
