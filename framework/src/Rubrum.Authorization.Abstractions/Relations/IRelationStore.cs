namespace Rubrum.Authorization.Relations;

public interface IRelationStore
{
    Task<bool> IsGrantedAsync(
        PermissionLink permission,
        ResourceReference resource,
        SubjectReference subject,
        CancellationToken ct = default);

    Task<bool> IsGrantedWithUserAsync(
        PermissionLink permission,
        ResourceReference resource,
        CancellationToken ct = default);

    Task GiveGrantAsync(
        Relation relation,
        ResourceReference resource,
        SubjectReference subject,
        CancellationToken ct = default);

    Task GiveGrantWithUserAsync(
        Relation relation,
        ResourceReference resource,
        CancellationToken ct = default);
}
