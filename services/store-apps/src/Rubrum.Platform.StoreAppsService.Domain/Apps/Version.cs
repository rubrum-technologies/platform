using Volo.Abp;
using Volo.Abp.Domain.Values;

namespace Rubrum.Platform.StoreAppsService.Apps;

public class Version : ValueObject
{
    public Version(int major, int minor, int patch)
    {
        Check.Positive(major, "major must be greater than 0");
        Major = major;

        Check.Positive(minor, "minor must be greater than 0");
        Minor = minor;

        Check.Positive(minor, "minor must be greater than 0");
        Patch = patch;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Version()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public int Major { get; }

    public int Minor { get; }

    public int Patch { get; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Major;
        yield return Minor;
        yield return Patch;
    }
}
