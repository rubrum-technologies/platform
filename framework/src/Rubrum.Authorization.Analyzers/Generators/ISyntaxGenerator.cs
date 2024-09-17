using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Rubrum.Authorization.Analyzers.Models;

namespace Rubrum.Authorization.Analyzers.Generators;

public interface ISyntaxGenerator
{
    void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<SyntaxInfo> syntaxInfos);
}
