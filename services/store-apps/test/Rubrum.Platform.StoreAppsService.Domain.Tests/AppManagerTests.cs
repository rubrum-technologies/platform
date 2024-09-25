using Rubrum.Platform.StoreAppsService.Apps;
using Shouldly;
using Xunit;
using static Rubrum.Platform.StoreAppsService.AppTestConstants;
using Version = Rubrum.Platform.StoreAppsService.Apps.Version;

namespace Rubrum.Platform.StoreAppsService;

public class AppManagerTests : StoreAppsServiceDomainTestBase
{
    private readonly AppManager _manager;

    public AppManagerTests()
    {
        _manager = GetRequiredService<AppManager>();
    }

    [Fact]
    public async Task CreateAsync()
    {
        var name = "Лучшее приложение";
        var version = new Version(1, 0, 0);
        var app = await _manager.CreateAsync(name, version, true);

        app.ShouldNotBeNull();
        app.Name.ShouldBe(name);
        app.Version.ShouldBe(version);
        app.Enabled.ShouldBe(true);
    }

    [Fact]
    public async Task CreateAsync_NameAlreadyExistsException()
    {
        await Assert.ThrowsAsync<AppNameAlreadyExistsException>(async () =>
        {
            await _manager.CreateAsync(TestName, TestVersion, true);
        });
    }
}
