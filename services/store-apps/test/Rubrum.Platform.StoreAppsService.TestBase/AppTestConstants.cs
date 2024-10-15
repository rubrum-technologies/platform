using Rubrum.Platform.StoreAppsService.Apps;

namespace Rubrum.Platform.StoreAppsService;

public static class AppTestConstants
{
    public static Guid TestAppId = Guid.NewGuid();

    public static Guid TestOwnerId => Guid.NewGuid();

    public static string TestName => "Тестовое приложение dataseed";

    public static AppVersion TestVersion => new(777, 999, 100500);

    public static Guid TestAppId2 = Guid.NewGuid();
}
