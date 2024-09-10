using Volo.Abp.Threading;

namespace Rubrum.Platform.StoreAppsService;

public static class StoreAppsServiceGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
        });
    }
}
