using Volo.Abp.EventBus;

namespace Rubrum.Authorization.Permissions;

[Serializable]
[EventName("Rubrum.Authorization.Permissions.GiveGrantPermission")]
public sealed record GiveGrantPermissionEto(
    string PermissionName,
    string ProviderName,
    string ProviderKey,
    bool IsGranted);
