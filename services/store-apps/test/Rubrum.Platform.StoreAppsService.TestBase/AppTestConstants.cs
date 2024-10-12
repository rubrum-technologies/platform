namespace Rubrum.Platform.StoreAppsService;

public static class AppTestConstants
{
    public static Guid TestAppId = Guid.NewGuid();

    public static Guid TestOwnerId => Guid.NewGuid();

    public static string TestName => "Тестовое приложение";

    public static Apps.Version TestVersion => new(1, 0, 0);
}
