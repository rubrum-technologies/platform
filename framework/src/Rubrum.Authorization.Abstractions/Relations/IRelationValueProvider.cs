namespace Rubrum.Authorization.Relations;

public interface IRelationValueProvider
{
    string Name { get; }

    Task<RelationGrantResult> GetResultAsync(
        Relationship relationship,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default);
}
