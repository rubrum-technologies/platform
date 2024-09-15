namespace Rubrum.Authorization.Permissions;

public interface IRelationValueProvider
{
    string Name { get; }

    Task<PermissionGrantResult> GetResultAsync(
        Relationship relationship,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default);
}
