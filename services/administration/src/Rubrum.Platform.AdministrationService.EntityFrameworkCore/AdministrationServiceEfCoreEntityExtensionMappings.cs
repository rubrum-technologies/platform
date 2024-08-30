using Volo.Abp.Threading;

namespace Rubrum.Platform.AdministrationService.EntityFrameworkCore;

public static class AdministrationServiceEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        AdministrationServiceGlobalFeatureConfigurator.Configure();
        AdministrationServiceModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
        });
    }
}
