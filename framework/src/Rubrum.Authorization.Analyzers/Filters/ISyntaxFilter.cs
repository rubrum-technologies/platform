using Microsoft.CodeAnalysis;

namespace Rubrum.Authorization.Analyzers.Filters;

public interface ISyntaxFilter
{
    bool IsMatch(SyntaxNode node);
}
