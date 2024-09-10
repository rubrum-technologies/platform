namespace Rubrum.Authorization.Permissions;

public interface IPermissionChecker
{
    Task<bool> IsGrantedAsync(
        Relationship relationship,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default);
}
