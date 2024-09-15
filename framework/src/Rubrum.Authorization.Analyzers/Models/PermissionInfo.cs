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
    }

    public string PropertyName { get; }
}
