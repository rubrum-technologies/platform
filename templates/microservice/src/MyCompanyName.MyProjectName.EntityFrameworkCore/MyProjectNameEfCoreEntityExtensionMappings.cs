using Volo.Abp.Threading;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

public static class MyProjectNameEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        MyProjectNameGlobalFeatureConfigurator.Configure();
        MyProjectNameModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
        });
    }
}
