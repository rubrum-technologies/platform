using Volo.Abp.Threading;

namespace Rubrum.Platform.BlobStorageService.EntityFrameworkCore;

public static class BlobStorageServiceEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        BlobStorageServiceGlobalFeatureConfigurator.Configure();
        BlobStorageServiceModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
        });
    }
}
