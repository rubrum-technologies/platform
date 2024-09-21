using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rubrum.Authorization.Analyzers.Filters;
using Rubrum.Authorization.Analyzers.Models;
using static System.StringComparison;
using static Rubrum.Authorization.Analyzers.WellKnownAttributes;

namespace Rubrum.Authorization.Analyzers.Inspectors;

public class DefinitionAttributeInspector : ISyntaxInspector
{
    public IReadOnlyList<ISyntaxFilter> Filters => [new TypeWithAttribute()];

    public bool TryHandle(GeneratorSyntaxContext context, [NotNullWhen(true)] out ISyntaxInfo? syntaxInfo)
    {
        if (context.Node is ClassDeclarationSyntax { AttributeLists.Count: > 0, } possibleType)
        {
            foreach (var attributeSyntax in possibleType.AttributeLists.SelectMany(a => a.Attributes))
            {
                var symbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol;

                if (symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }

                var fullName = attributeSymbol.ContainingType.ToDisplayString();

                if (fullName.StartsWith(DefinitionAttribute, Ordinal) &&
                    context.SemanticModel.GetDeclaredSymbol(possibleType) is ITypeSymbol typeSymbol)
                {
                    syntaxInfo = new DefinitionInfo(
                        typeSymbol,
                        GetRelations(typeSymbol),
                        GetPermissions(typeSymbol));
                    return true;
                }
            }
        }

        syntaxInfo = null;
        return false;
    }

    private static ImmutableArray<RelationInfo> GetRelations(ITypeSymbol typeSymbol)
    {
        var builder = ImmutableArray.CreateBuilder<RelationInfo>();

        foreach (var attributeData in from attributeData in typeSymbol.GetAttributes()
                 let fullName = attributeData.AttributeClass?.ToDisplayString() ?? string.Empty
                 where fullName.StartsWith(RelationAttribute, Ordinal)
                 select attributeData)
        {
            builder.Add(new RelationInfo(attributeData));
        }

        return builder.ToImmutable();
    }

    private static ImmutableArray<PermissionInfo> GetPermissions(ITypeSymbol typeSymbol)
    {
        var builder = ImmutableArray.CreateBuilder<PermissionInfo>();

        foreach (var attributeData in from attributeData in typeSymbol.GetAttributes()
                 let fullName = attributeData.AttributeClass?.ToDisplayString() ?? string.Empty
                 where fullName.StartsWith(PermissionAttribute, Ordinal)
                 select attributeData)
        {
            builder.Add(new PermissionInfo(attributeData));
        }

        return builder.ToImmutable();
    }
}
