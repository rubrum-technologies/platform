using Rubrum.Platform.BlobStorageService.Folders;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobManager(
    IFolderBlobRepository folderRepository,
    IBlobContainerFactory blobContainerFactory) : DomainService
{
    public async Task<IRemoteStreamContent> GetFileAsync(Blob blob, CancellationToken ct = default)
    {
        var blobContainer = blobContainerFactory.Create(blob);

        return new RemoteStreamContent(
            await blobContainer.GetAsync(blob.SystemFileName, ct),
            blob.Metadata.Filename,
            blob.Metadata.MimeType);
    }

    public async Task<Blob> CreateAsync(
        Guid ownerId,
        Guid? folderId,
        IRemoteStreamContent content,
        CancellationToken ct = default)
    {
        Check.NotNullOrWhiteSpace(content.FileName, nameof(content.FileName));

        if (folderId is not null)
        {
            await CheckFolderAsync(ownerId, folderId.Value);
        }

        await using var file = content.GetStream();

        var metadata = new BlobMetadata
        {
            Size = file.Length,
            MimeType = content.ContentType,
            Filename = content.FileName,
            Extension = content.FileName.Split(".")[^1],
        };
        var blob = new Blob(GuidGenerator.Create(), CurrentTenant.Id, ownerId, folderId, metadata);
        var blobContainer = blobContainerFactory.Create(blob);

        await blobContainer.SaveAsync(blob.SystemFileName, file, true, ct);

        return blob;
    }

    private async Task CheckFolderAsync(Guid ownerId, Guid folderId)
    {
        if (!await folderRepository.AnyAsync(x => x.Id == folderId && x.OwnerId == ownerId))
        {
            throw new BlobFolderHasDifferentOwnerException(ownerId, folderId);
        }
    }
}
