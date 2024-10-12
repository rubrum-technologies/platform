using Rubrum.Platform.StoreAppsService.Apps;
using Shouldly;
using Volo.Abp.Uow;
using Xunit;
using static Rubrum.Platform.StoreAppsService.AppTestConstants;
using Version = Rubrum.Platform.StoreAppsService.Apps.Version;

namespace Rubrum.Platform.StoreAppsService;

public class AppManagerTests : StoreAppsServiceDomainTestBase
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly AppManager _manager;
    private readonly IAppRepository _repository;

    public AppManagerTests()
    {
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        _manager = GetRequiredService<AppManager>();
        _repository = GetRequiredService<IAppRepository>();
    }

    [Fact]
    public async Task CreateAsync()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        var name = "Лучшее приложение";
        var version = new Version(1, 0, 0);
        var app = await _manager.CreateAsync(TestOwnerId, name, version, true);

        app.ShouldNotBeNull();
        app.Name.ShouldBe(name);
        app.Version.ShouldBe(version);
        app.Enabled.ShouldBe(true);
    }

    [Fact]
    public async Task CreateAsync_NameAlreadyExistsException()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        await Assert.ThrowsAsync<AppNameAlreadyExistsException>(async () =>
        {
            await _manager.CreateAsync(Guid.NewGuid(), TestName, TestVersion, true);
        });
    }

    [Fact]
    public async Task ChangeNameAsync()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        var source = await _repository.GetAsync(x => x.Name == TestName);

        await _manager.ChangeNameAsync(source, "TestChangeName");

        source.ShouldNotBeNull();
        source.Name.ShouldBe("TestChangeName");
    }

    [Fact]
    public async Task ChangeNameAsync_AppNameAlreadyExistsException()
    {
        using var uow = _unitOfWorkManager.Begin(true, true);

        var source = await _manager.CreateAsync(Guid.NewGuid(), "TestDuplicate", TestVersion, false);

        await Assert.ThrowsAsync<AppNameAlreadyExistsException>(async () =>
        {
            await _manager.ChangeNameAsync(source, TestName);
        });
    }
}
