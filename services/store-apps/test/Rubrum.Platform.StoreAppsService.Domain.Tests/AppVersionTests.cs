using Rubrum.Platform.StoreAppsService.Apps;
using Xunit;

namespace Rubrum.Platform.StoreAppsService;

public class AppVersionTests : StoreAppsServiceDomainTestBase
{
    [Theory]
    [InlineData(-19021)]
    [InlineData(unchecked(int.MaxValue + 1))]
    public void SetMajor_IncorrectRange(int major)
    {
        Assert.Throws<ArgumentException>(() => { var version = new AppVersion(major, 200, 300); });
    }

    [Theory]
    [InlineData(-190)]
    [InlineData(unchecked(int.MaxValue + 1))]
    public void SetMinor_IncorrectRange(int minor)
    {
        Assert.Throws<ArgumentException>(() => { var version = new AppVersion(111, minor, 190); });
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(unchecked(int.MaxValue + 1))]
    public void SetPatch_IncorrectRange(int patch)
    {
        Assert.Throws<ArgumentException>(() => { var version = new AppVersion(10, 200, patch); });
    }
}
