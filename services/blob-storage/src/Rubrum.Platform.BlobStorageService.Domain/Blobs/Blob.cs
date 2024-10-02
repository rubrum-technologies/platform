using Rubrum.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class Blob : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMustHaveOwner
{
    internal Blob(
        Guid id,
        Guid? tenantId,
        Guid ownerId,
        Guid? folderId,
        BlobMetadata metadata)
        : base(id)
    {
        TenantId = tenantId;
        OwnerId = ownerId;
        FolderId = folderId;
        Metadata = metadata;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Blob()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid? TenantId { get; }

    public Guid OwnerId { get; }

    public Guid? FolderId { get; internal set; }

    public BlobMetadata Metadata { get; internal set; }

    internal string SystemFileName => $"{Id}.{Metadata.Extension}";
}
