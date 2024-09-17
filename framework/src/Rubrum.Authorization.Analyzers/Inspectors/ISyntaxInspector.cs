using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Rubrum.Authorization.Analyzers.Filters;
using Rubrum.Authorization.Analyzers.Models;

namespace Rubrum.Authorization.Analyzers.Inspectors;

public interface ISyntaxInspector
{
    IReadOnlyList<ISyntaxFilter> Filters { get; }

    bool TryHandle(
        GeneratorSyntaxContext context,
        [NotNullWhen(true)] out SyntaxInfo? syntaxInfo);
}
