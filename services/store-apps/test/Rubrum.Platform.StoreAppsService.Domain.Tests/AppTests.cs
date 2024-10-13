using Rubrum.Platform.StoreAppsService.Apps;
using Shouldly;
using Xunit;
using Version = Rubrum.Platform.StoreAppsService.Apps.Version;

namespace Rubrum.Platform.StoreAppsService;

public class AppTests : StoreAppsServiceDomainTestBase
{
    private readonly App _app = new(
        Guid.NewGuid(),
        Guid.NewGuid(),
        Guid.NewGuid(),
        "InternalName",
        new Version(10, 255, 323),
        true);

    [Theory]
    [InlineData("Test_Name")]
    [InlineData("PlatformApp")]
    [InlineData("LongAppNameForTesting.Hello.World")]
    [InlineData("A")]
    public void SetName(string name)
    {
        _app.SetName(name);

        _app.Name.ShouldBe(name);
    }

    [Fact]
    public void SetName_Empty()
    {
        Assert.Throws<ArgumentException>(() => { _app.SetName(string.Empty.PadRight(10)); });
    }

    [Fact]
    public void SetName_MaxLength()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            _app.SetName(string.Empty.PadRight(AppConstants.MaxNameLength + 1, '-'));
        });
    }

    [Fact]
    public void Deactivate()
    {
       _app.Deactivate();

       _app.Enabled.ShouldBeFalse();
    }

    [Fact]
    public void Activate()
    {
        _app.Deactivate();
        _app.Activate();

        _app.Enabled.ShouldBeTrue();
    }
}
