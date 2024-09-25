namespace Rubrum.Authorization.Relations;

public static class RelationCheckerExtensions
{
    public static Task IsGrantedWithUserAsync<T>(
        this IRelationChecker checker,
        PermissionLink permission,
        string id,
        CancellationToken ct = default)
    {
        return checker.IsGrantedWithUserAsync(
            permission,
            new ResourceReference(typeof(T).Name, id),
            ct);
    }

    public static Task IsGrantedWithUserAsync<T>(
        this IRelationChecker checker,
        PermissionLink permission,
        int id,
        CancellationToken ct = default)
    {
        return checker.IsGrantedWithUserAsync<T>(permission, id.ToString(), ct);
    }

    public static Task IsGrantedWithUserAsync<T>(
        this IRelationChecker checker,
        PermissionLink permission,
        long id,
        CancellationToken ct = default)
    {
        return checker.IsGrantedWithUserAsync<T>(permission, id.ToString(), ct);
    }

    public static Task IsGrantedWithUserAsync<T>(
        this IRelationChecker checker,
        PermissionLink permission,
        Guid id,
        CancellationToken ct = default)
    {
        return checker.IsGrantedWithUserAsync<T>(permission, id.ToString(), ct);
    }
}
