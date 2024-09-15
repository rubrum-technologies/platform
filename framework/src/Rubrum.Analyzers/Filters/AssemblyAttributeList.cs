using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rubrum.Analyzers.Filters;

public sealed class AssemblyAttributeList : ISyntaxFilter
{
    public bool IsMatch(SyntaxNode node)
        => node is AttributeListSyntax;
}
