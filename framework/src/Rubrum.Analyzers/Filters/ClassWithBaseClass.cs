using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rubrum.Analyzers.Filters;

public sealed class ClassWithBaseClass : ISyntaxFilter
{
    public bool IsMatch(SyntaxNode node)
        => node is ClassDeclarationSyntax { BaseList.Types.Count: > 0 };
}
