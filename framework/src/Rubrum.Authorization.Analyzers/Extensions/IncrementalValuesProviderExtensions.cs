using Microsoft.CodeAnalysis;
using Rubrum.Authorization.Analyzers.Models;

namespace Rubrum.Authorization.Analyzers.Extensions;

public static class IncrementalValuesProviderExtensions
{
    public static IncrementalValuesProvider<ISyntaxInfo> WhereNotNull(
        this IncrementalValuesProvider<ISyntaxInfo?> source)
        => source.Where(static t => t is not null)!;
}
