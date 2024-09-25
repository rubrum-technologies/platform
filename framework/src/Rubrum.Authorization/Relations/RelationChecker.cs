using Volo.Abp.Authorization;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Authorization.Relations;

public class RelationChecker(IRelationStore store) : IRelationChecker, ITransientDependency
{
    public async Task IsGrantedAsync(
        PermissionLink permission,
        ResourceReference resource,
        SubjectReference subject,
        CancellationToken ct = default)
    {
        var result = await store.IsGrantedAsync(permission, resource, subject, ct);

        if (!result)
        {
            throw new AbpAuthorizationException(code: AbpAuthorizationErrorCodes.GivenPolicyHasNotGranted);
        }
    }

    public async Task IsGrantedWithUserAsync(
        PermissionLink permission,
        ResourceReference resource,
        CancellationToken ct = default)
    {
        var result = await store.IsGrantedWithUserAsync(permission, resource, ct);

        if (!result)
        {
            throw new AbpAuthorizationException(code: AbpAuthorizationErrorCodes.GivenPolicyHasNotGranted);
        }
    }
}
