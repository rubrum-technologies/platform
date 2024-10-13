namespace Rubrum.Platform.StoreAppsService;

public static class AppTestConstants
{
    public static Guid TestAppId = Guid.NewGuid();

    public static Guid TestOwnerId => Guid.NewGuid();

    public static string TestName => "Тестовое приложение dataseed";

    public static Apps.Version TestVersion => new(777, 999, 100500);
}
