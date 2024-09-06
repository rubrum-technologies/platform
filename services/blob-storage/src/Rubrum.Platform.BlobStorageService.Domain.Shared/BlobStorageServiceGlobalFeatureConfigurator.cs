using Volo.Abp.Threading;

namespace Rubrum.Platform.BlobStorageService;

public static class BlobStorageServiceGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
        });
    }
}
