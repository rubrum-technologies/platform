using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rubrum.Authorization.Analyzers.Models;

public class PermissionInfo
{
    public PermissionInfo(AttributeSyntax attributeSyntax)
    {
        var arguments = attributeSyntax.ArgumentList!.Arguments;

        PropertyName = arguments[0]
            .ToFullString()
            .Trim('"');

        AttributeSyntax = attributeSyntax;
    }

    public string PropertyName { get; }

    public AttributeSyntax AttributeSyntax { get; }

    internal bool Equals(PermissionInfo other)
    {
        return AttributeSyntax.IsEquivalentTo(other.AttributeSyntax);
    }
}
