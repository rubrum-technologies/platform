namespace Rubrum.Authorization.Permissions;

public interface IPermissionManager
{
    Task CreateOrUpdateAsync(IEnumerable<CreatePermissionGroupDefinitionEto> groups, CancellationToken ct = default);

    Task CreateOrUpdateAsync(IEnumerable<CreatePermissionDefinitionEto> permissions, CancellationToken ct = default);

    Task SetAsync(GiveGrantPermissionEto grant, CancellationToken ct = default);
}
