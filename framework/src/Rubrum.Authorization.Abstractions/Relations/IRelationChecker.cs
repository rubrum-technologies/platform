namespace Rubrum.Authorization.Relations;

public interface IRelationChecker
{
    Task<bool> IsGrantedAsync(
        Relationship relationship,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default);

    Task<bool> IsGrantedWithUserAsync(
        PermissionLink relation,
        ResourceReference resource,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default);
}
