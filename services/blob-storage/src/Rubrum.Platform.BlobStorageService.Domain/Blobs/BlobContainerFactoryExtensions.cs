using Volo.Abp.BlobStoring;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public static class BlobContainerFactoryExtensions
{
    public static IBlobContainer Create(this IBlobContainerFactory factory)
    {
        return factory.Create("files");
    }
}
