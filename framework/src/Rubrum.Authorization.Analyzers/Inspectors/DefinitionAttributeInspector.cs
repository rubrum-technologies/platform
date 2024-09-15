using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rubrum.Analyzers.Filters;
using Rubrum.Analyzers.Inspectors;
using Rubrum.Analyzers.Models;
using Rubrum.Authorization.Analyzers.Models;
using static System.StringComparison;
using static Rubrum.Authorization.Analyzers.WellKnownAttributes;
using static Rubrum.Authorization.Analyzers.WellKnownClasses;

namespace Rubrum.Authorization.Analyzers.Inspectors;

public class DefinitionAttributeInspector : ISyntaxInspector
{
    public IReadOnlyList<ISyntaxFilter> Filters => [new TypeWithAttribute()];

    public bool TryHandle(GeneratorSyntaxContext context, [NotNullWhen(true)] out SyntaxInfo? syntaxInfo)
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
                        GetRelations(context, possibleType, typeSymbol),
                        GetPermissions(context, possibleType));
                    return true;
                }
            }
        }

        syntaxInfo = null;
        return false;
    }

    private static ImmutableArray<RelationInfo> GetRelations(
        GeneratorSyntaxContext context,
        ClassDeclarationSyntax typeSyntax,
        ITypeSymbol typeSymbol)
    {
        var builder = ImmutableArray.CreateBuilder<RelationInfo>();

        foreach (var attributeSyntax in typeSyntax.AttributeLists.SelectMany(a => a.Attributes))
        {
            var symbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol;

            if (symbol is not IMethodSymbol attributeSymbol)
            {
                continue;
            }

            var fullName = attributeSymbol.ContainingType.ToDisplayString();

            if (fullName.StartsWith(RelationAttribute, Ordinal) &&
                context.SemanticModel.GetDeclaredSymbol(typeSyntax) is ITypeSymbol)
            {
                builder.Add(new RelationInfo(context, typeSymbol, attributeSyntax));
            }
        }

        return builder.ToImmutable();
    }

    private static ImmutableArray<PermissionInfo> GetPermissions(
        GeneratorSyntaxContext context,
        ClassDeclarationSyntax typeSyntax)
    {
        var builder = ImmutableArray.CreateBuilder<PermissionInfo>();

        foreach (var property in typeSyntax.Members.OfType<PropertyDeclarationSyntax>())
        {
            var propertyTypeSymbol = context.SemanticModel.GetSymbolInfo(property.Type).Symbol!;
            var fullName = propertyTypeSymbol.ToDisplayString();

            if (fullName != PermissionClass)
            {
                continue;
            }

            builder.Add(new PermissionInfo(property.Identifier.Text));
        }

        return builder.ToImmutable();
    }
}
