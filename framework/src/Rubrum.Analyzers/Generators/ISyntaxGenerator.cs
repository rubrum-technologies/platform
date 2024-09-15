using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Rubrum.Analyzers.Models;

namespace Rubrum.Analyzers.Generators;

public interface ISyntaxGenerator
{
    void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<SyntaxInfo> syntaxInfos);
}
