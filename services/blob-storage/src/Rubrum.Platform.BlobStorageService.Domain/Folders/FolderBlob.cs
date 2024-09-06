using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rubrum.Platform.BlobStorageService.Folders;

public class FolderBlob : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    internal FolderBlob(
        Guid id,
        Guid? tenantId,
        Guid? parentId,
        string name)
        : base(id)
    {
        TenantId = tenantId;
        ParentId = parentId;
        SetName(name);
    }

    public Guid? TenantId { get; }

    public Guid? ParentId { get; internal set; }

    public string Name { get; protected set; }

    [MemberNotNull(nameof(Name))]
    internal void SetName(string fileName)
    {
        Name = Check.NotNullOrWhiteSpace(fileName, nameof(fileName), FolderBlobConstants.NameLength);
    }
}
