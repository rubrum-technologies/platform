using Volo.Abp.Application.Dtos;

namespace Rubrum.Platform.StoreAppsService;

public class AppDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; init; }

    public string Version { get; init; }

    public bool Enabled { get; init; }
}
