using Volo.Abp.Threading;

namespace Rubrum.Platform.AdministrationService;

public static class AdministrationServiceGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() => { });
    }
}
