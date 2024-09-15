using Microsoft.CodeAnalysis;

namespace Rubrum.Analyzers.Filters;

public interface ISyntaxFilter
{
    bool IsMatch(SyntaxNode node);
}
