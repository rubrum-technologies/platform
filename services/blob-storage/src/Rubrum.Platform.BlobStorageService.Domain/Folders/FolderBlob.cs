using System.Diagnostics.CodeAnalysis;
using Rubrum.Auditing;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Platform.BlobStorageService.Folders;

public class FolderBlob : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMustHaveOwner
{
    internal FolderBlob(
        Guid id,
        Guid? tenantId,
        Guid ownerId,
        Guid? parentId,
        string name)
        : base(id)
    {
        TenantId = tenantId;
        OwnerId = ownerId;
        ParentId = parentId;
        SetName(name);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected FolderBlob()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid? TenantId { get; }

    public Guid OwnerId { get; }

    public Guid? ParentId { get; internal set; }

    public string Name { get; protected set; }

    [MemberNotNull(nameof(Name))]
    internal void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), FolderBlobConstants.NameLength);
    }
}
