using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rubrum.Authorization.Analyzers.Filters;

public sealed class TypeWithAttribute : ISyntaxFilter
{
    public bool IsMatch(SyntaxNode node)
        => node is BaseTypeDeclarationSyntax { AttributeLists.Count: > 0 };
}
