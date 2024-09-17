using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class App : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    internal App(Guid id, Guid? tenantId, string name, Version version, bool enabled)
        : base(id)
    {
        TenantId = tenantId;
        SetName(name);
        Version = version;
        Enabled = enabled;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected App()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public string Name { get; private set; }

    public Version Version { get; private set; }

    public bool Enabled { get; private set; }

    public Guid? TenantId { get; }

    public void Activate()
    {
        Enabled = true;
    }

    public void Deactivate()
    {
        Enabled = false;
    }

    [MemberNotNull(nameof(Name))]
    internal void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), AppConstants.MaxNameLength);
    }
}
