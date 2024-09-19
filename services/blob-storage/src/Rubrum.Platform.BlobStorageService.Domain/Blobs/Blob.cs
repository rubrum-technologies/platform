using System.Diagnostics.CodeAnalysis;
using Rubrum.Auditing;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class Blob : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMustHaveOwner, IHasEntityVersion
{
    internal Blob(
        Guid id,
        Guid? tenantId,
        Guid ownerId,
        string fileName)
        : base(id)
    {
        SetFileName(fileName);
        TenantId = tenantId;
        OwnerId = ownerId;
        IsDisposable = true;
    }

    public Guid? TenantId { get; }

    public Guid OwnerId { get; }

    public string FileName { get; protected set; }

    public string Extension => FileName.Split('.')[^1];

    public string SystemFileName => $".{Extension}";

    public bool IsDisposable { get; protected set; }

    public int EntityVersion { get; protected set; }

    [MemberNotNull(nameof(FileName))]
    internal void SetFileName(string fileName)
    {
        FileName = Check.NotNullOrWhiteSpace(fileName, nameof(fileName), BlobConstants.FileNameLength);
    }

    internal void MarkAsPermanent()
    {
        IsDisposable = false;
    }
}
