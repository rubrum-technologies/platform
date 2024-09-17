namespace Rubrum.Authorization.Analyzers.Models;

public sealed class SyntaxInfoComparer : IEqualityComparer<SyntaxInfo>
{
    public static SyntaxInfoComparer Default { get; } = new();

    public bool Equals(SyntaxInfo? x, SyntaxInfo? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Equals(y);
    }

    public int GetHashCode(SyntaxInfo obj)
        => obj.GetHashCode();
}
