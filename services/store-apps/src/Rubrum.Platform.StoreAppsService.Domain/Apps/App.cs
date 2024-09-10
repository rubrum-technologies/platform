using Volo.Abp.Domain.Entities.Auditing;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class App : FullAuditedAggregateRoot<Guid>
{
    internal App(Guid id, string name, string version, bool enabled)
    : base(id)
    {
        Name = name;
        Version = version;
        Enabled = enabled;
    }

    public string Name { get; private set; }

    public string Version { get; private set; }

    public bool Enabled { get; private set; }
}
