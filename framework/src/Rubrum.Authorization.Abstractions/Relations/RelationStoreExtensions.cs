namespace Rubrum.Authorization.Relations;

public static class RelationStoreExtensions
{
    public static Task GiveGrantWithUserAsync<T>(
        this IRelationStore store,
        Relation relation,
        string id,
        CancellationToken ct = default)
    {
        return store.GiveGrantWithUserAsync(
            relation,
            new ResourceReference(typeof(T).Name, id),
            ct);
    }

    public static Task GiveGrantWithUserAsync<T>(
        this IRelationStore store,
        Relation relation,
        int id,
        CancellationToken ct = default)
    {
        return store.GiveGrantWithUserAsync<T>(relation, id.ToString(), ct);
    }

    public static Task GiveGrantWithUserAsync<T>(
        this IRelationStore store,
        Relation relation,
        long id,
        CancellationToken ct = default)
    {
        return store.GiveGrantWithUserAsync<T>(relation, id.ToString(), ct);
    }

    public static Task GiveGrantWithUserAsync<T>(
        this IRelationStore store,
        Relation relation,
        Guid id,
        CancellationToken ct = default)
    {
        return store.GiveGrantWithUserAsync<T>(relation, id.ToString(), ct);
    }
}
