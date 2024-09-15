using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rubrum.Authorization.Analyzers.Models;

public sealed class RelationInfo
{
    public RelationInfo(
        GeneratorSyntaxContext context,
        ITypeSymbol typeSymbol,
        AttributeSyntax attributeSyntax)
    {
        var arguments = attributeSyntax.ArgumentList!.Arguments;

        PropertyName = arguments[0]
            .ToFullString()
            .Trim('"');
        ClassName = $"{PropertyName}Relation";
        FullName = $"{typeSymbol.ToDisplayString()}.{ClassName}";

        Classes = GetValues(context, attributeSyntax);
    }

    public string FullName { get; }

    public string ClassName { get; }

    public string PropertyName { get; }

    public ImmutableArray<string> Classes { get; }

    private static ImmutableArray<string> GetValues(
        GeneratorSyntaxContext context,
        AttributeSyntax attributeSyntax)
    {
        var builder = ImmutableArray.CreateBuilder<string>();

        foreach (var argument in attributeSyntax.ArgumentList!.Arguments.Skip(1))
        {
            if (argument.Expression is not TypeOfExpressionSyntax expression)
            {
                continue;
            }

            var typeSymbol = context.SemanticModel.GetTypeInfo(expression.Type).Type!;

            builder.Add(typeSymbol.ToDisplayString());
        }

        return builder.ToImmutable();
    }
}
