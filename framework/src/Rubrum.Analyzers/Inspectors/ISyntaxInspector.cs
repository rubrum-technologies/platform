using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Rubrum.Analyzers.Filters;
using Rubrum.Analyzers.Models;

namespace Rubrum.Analyzers.Inspectors;

public interface ISyntaxInspector
{
    IReadOnlyList<ISyntaxFilter> Filters { get; }

    bool TryHandle(
        GeneratorSyntaxContext context,
        [NotNullWhen(true)] out SyntaxInfo? syntaxInfo);
}
