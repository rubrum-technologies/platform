using System.Diagnostics.CodeAnalysis;
using Rubrum.Auditing;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Platform.WindowsService.Windows;

public class Window : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMustHaveOwner
{
    internal Window(
        Guid id,
        Guid? tenantId,
        Guid ownerId,
        string name,
        Guid appId,
        WindowPosition position,
        WindowSize size)
        : base(id)
    {
        TenantId = tenantId;
        OwnerId = ownerId;
        SetName(name);
        AppId = appId;
        Position = position;
        Size = size;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Window()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid? TenantId { get; }

    public Guid OwnerId { get; }

    public string Name { get; private set; }

    public Guid AppId { get; private set; }

    public WindowPosition Position { get; set; }

    public WindowSize Size { get; set; }


    [MemberNotNull(nameof(Name))]
    internal void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), WindowsConstants.MaxNameLength);
    }
}
