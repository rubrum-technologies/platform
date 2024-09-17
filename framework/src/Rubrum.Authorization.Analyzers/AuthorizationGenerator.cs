using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Rubrum.Authorization.Analyzers.Extensions;
using Rubrum.Authorization.Analyzers.Filters;
using Rubrum.Authorization.Analyzers.Generators;
using Rubrum.Authorization.Analyzers.Inspectors;
using Rubrum.Authorization.Analyzers.Models;

namespace Rubrum.Authorization.Analyzers;

[Generator]
public class AuthorizationGenerator : IIncrementalGenerator
{
    private static readonly ISyntaxInspector[] Inspectors =
    [
        new DefinitionAttributeInspector(),
    ];

    private static readonly ISyntaxGenerator[] Generators =
    [
        new DefinitionGenerator(),
    ];

    private static readonly Func<SyntaxNode, bool>? Predicate;

#pragma warning disable S3963
    static AuthorizationGenerator()
#pragma warning restore S3963
    {
        var filterBuilder = new SyntaxFilterBuilder();

        foreach (var inspector in Inspectors)
        {
            filterBuilder.AddRange(inspector.Filters);
        }

        Predicate = filterBuilder.Build();
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var syntaxInfos =
            context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => Predicate?.Invoke(s) ?? false,
                    transform: static (ctx, _) => Transform(ctx))
                .WhereNotNull()
                .Collect();

        var valueProvider = context.CompilationProvider.Combine(syntaxInfos);

        context.RegisterSourceOutput(
            valueProvider,
            static (context, source) => Execute(context, source.Left, source.Right));
    }

    private static void Execute(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<SyntaxInfo> syntaxInfos)
    {
        foreach (var generator in Generators.AsSpan())
        {
            generator.Generate(context, compilation, syntaxInfos);
        }
    }

    private static SyntaxInfo? Transform(GeneratorSyntaxContext context)
    {
        foreach (var inspector in Inspectors)
        {
            if (inspector.TryHandle(context, out var syntaxInfo))
            {
                return syntaxInfo;
            }
        }

        return null;
    }
}
