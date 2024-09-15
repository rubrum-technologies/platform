using Volo.Abp.DependencyInjection;

namespace Rubrum.Authorization.Permissions;

public class RelationChecker(
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
}
