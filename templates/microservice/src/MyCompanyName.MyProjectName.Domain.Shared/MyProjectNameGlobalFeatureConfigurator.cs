using Volo.Abp.Threading;

namespace MyCompanyName.MyProjectName;

public static class MyProjectNameGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
        });
    }
}
