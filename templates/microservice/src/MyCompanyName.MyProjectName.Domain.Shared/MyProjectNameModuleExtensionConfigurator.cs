using Volo.Abp.Threading;

namespace MyCompanyName.MyProjectName;

public static class MyProjectNameModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {
        // Method intentionally left empty.
    }

    private static void ConfigureExtraProperties()
    {
        // Method intentionally left empty.
    }
}
