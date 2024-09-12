using Volo.Abp;
using Volo.Abp.Domain.Values;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class Version : ValueObject
{
    public int Major { get; private set; }

    public int Minor { get; private set; }

    public int Patch { get; private set; }

    private Version()
    {
    }

    public Version(int major, int minor, int patch)
    {
        Check.Positive(major, "major must be greater than 0");
        Major = major;

        Check.Positive(minor, "minor must be greater than 0");
        Minor = minor;

        Check.Positive(minor, "minor must be greater than 0");
        Patch = patch;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Major;
        yield return Minor;
        yield return Patch;
    }
}
