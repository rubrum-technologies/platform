using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Rubrum.Authorization.Relations;

public class RelationChecker(
    ICurrentUser currentUser,
    IRelationValueProviderManager relationValueProviderManager) : IRelationChecker, ITransientDependency
{
    public async Task<bool> IsGrantedAsync(
        Relationship relationship,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default)
    {
        var isGranted = false;

        foreach (var provider in relationValueProviderManager.ValueProviders)
        {
            var result = await provider.GetResultAsync(relationship, context, ct);

            if (result == RelationGrantResult.Granted)
            {
                isGranted = true;
            }
            else if (result == RelationGrantResult.Prohibited)
            {
                return false;
            }
        }

        return isGranted;
    }

    public Task<bool> IsGrantedWithUserAsync(
        PermissionLink relation,
        ResourceReference resource,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default)
    {
        return IsGrantedAsync(
            new Relationship(
                relation,
                resource,
                new SubjectReference("UserDefinition", currentUser.GetId().ToString())),
            context,
            ct);
    }
}
