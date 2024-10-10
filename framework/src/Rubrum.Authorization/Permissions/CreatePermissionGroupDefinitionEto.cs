using Volo.Abp.EventBus;

namespace Rubrum.Authorization.Permissions;

[Serializable]
[EventName("Rubrum.Authorization.Permissions.CreatePermissionGroupDefinition")]
public sealed record CreatePermissionGroupDefinitionEto(string Name, string? DisplayName);
