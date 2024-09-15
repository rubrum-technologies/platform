using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rubrum.Analyzers.Filters;

public sealed class MethodWithAttribute : ISyntaxFilter
{
    public bool IsMatch(SyntaxNode node)
        => node is MethodDeclarationSyntax { AttributeLists.Count: > 0 };
}
