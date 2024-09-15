using Microsoft.CodeAnalysis;
using Rubrum.Analyzers.Models;

namespace Rubrum.Analyzers.Extensions;

public static class IncrementalValuesProviderExtensions
{
    public static IncrementalValuesProvider<SyntaxInfo> WhereNotNull(
        this IncrementalValuesProvider<SyntaxInfo?> source)
        => source.Where(static t => t is not null)!;
}
