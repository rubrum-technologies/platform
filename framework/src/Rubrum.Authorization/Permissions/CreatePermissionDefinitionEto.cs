using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Authorization.Permissions;

[Serializable]
[EventName("Rubrum.Authorization.Permissions.CreatePermissionDefinition")]
public sealed record CreatePermissionDefinitionEto(
    string GroupName,
    string Name,
    string? ParentName,
    string? DisplayName,
    bool IsEnabled,
    MultiTenancySides MultiTenancySide,
    string? Providers,
    string? StateCheckers);
