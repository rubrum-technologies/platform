namespace Rubrum.Authorization.Permissions;

public interface IRelationChecker
{
    Task<bool> IsGrantedAsync(
        Relationship relationship,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default);
}
