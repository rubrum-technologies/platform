namespace Rubrum.Authorization.Relations;

public interface IRelationChecker
{
    Task IsGrantedAsync(
        PermissionLink permission,
        ResourceReference resource,
        SubjectReference subject,
        CancellationToken ct = default);

    Task IsGrantedWithUserAsync(
        PermissionLink permission,
        ResourceReference resource,
        CancellationToken ct = default);
}
