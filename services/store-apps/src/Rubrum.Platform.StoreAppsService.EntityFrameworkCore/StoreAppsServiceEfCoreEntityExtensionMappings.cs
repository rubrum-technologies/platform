using Volo.Abp.Threading;

namespace Rubrum.Platform.StoreAppsService.EntityFrameworkCore;

public static class StoreAppsServiceEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        StoreAppsServiceGlobalFeatureConfigurator.Configure();
        StoreAppsServiceModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
        });
    }
}
