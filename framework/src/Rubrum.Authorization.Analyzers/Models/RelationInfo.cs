using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rubrum.Authorization.Analyzers.Models;

public sealed class RelationInfo
{
    public RelationInfo(
        AttributeSyntax attributeSyntax,
        IMethodSymbol attributeSymbol)
    {
        PropertyName = attributeSyntax.ArgumentList!.Arguments[0]
            .ToString()
            .Trim('"');
        ClassName = $"{PropertyName}Relation";

        Values = GetValues(attributeSymbol);
        AttributeSyntax = attributeSyntax;
        AttributeSymbol = attributeSymbol;
    }

    public string ClassName { get; }

    public string PropertyName { get; }

    public ImmutableArray<ITypeSymbol> Values { get; }

    public AttributeSyntax AttributeSyntax { get; }

    public IMethodSymbol AttributeSymbol { get; }

    private static ImmutableArray<ITypeSymbol> GetValues(IMethodSymbol attributeSymbol)
    {
        var builder = ImmutableArray.CreateBuilder<ITypeSymbol>();

        foreach (var typeSymbol in attributeSymbol.ContainingType.TypeArguments)
        {
            builder.Add(typeSymbol);
        }

        return builder.ToImmutable();
    }
}
