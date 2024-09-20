using Volo.Abp.BlobStoring;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public static class BlobContainerFactoryExtensions
{
    public static IBlobContainer Create(this IBlobContainerFactory blobContainerFactory, Blob blob)
    {
        return blobContainerFactory.Create(blob.OwnerId.ToString());
    }
}
