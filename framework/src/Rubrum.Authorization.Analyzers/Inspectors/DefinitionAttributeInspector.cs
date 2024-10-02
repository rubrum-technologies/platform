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
            AttributeSyntax? definitionSyntax = null;
            var relationsSyntax = new List<AttributeSyntax>();
            var permissionsSyntax = new List<AttributeSyntax>();

            var attributesSyntax = possibleType.AttributeLists.SelectMany(a => a.Attributes).ToList();

            foreach (var attributeSyntax in attributesSyntax)
            {
                var symbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol;

                if (symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }

                var fullName = attributeSymbol.ContainingType.ToDisplayString();

                if (fullName.StartsWith(DefinitionAttribute, Ordinal))
                {
                    definitionSyntax = attributeSyntax;
                }

                if (fullName.StartsWith(RelationAttribute, Ordinal))
                {
                    relationsSyntax.Add(attributeSyntax);
                }

                if (fullName.StartsWith(PermissionAttribute, Ordinal))
                {
                    permissionsSyntax.Add(attributeSyntax);
                }
            }

            if (definitionSyntax is null)
            {
                syntaxInfo = null;
                return false;
            }

            var relations = relationsSyntax
                .Select(syntax => new RelationInfo(
                    syntax,
                    (IMethodSymbol)context.SemanticModel.GetSymbolInfo(syntax).Symbol!))
                .ToImmutableArray();
            var permissions = permissionsSyntax.Select(syntax => new PermissionInfo(syntax))
                .ToImmutableArray();

            syntaxInfo = new DefinitionInfo(
                (ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(possibleType)!,
                relations,
                permissions);
            return true;
        }

        syntaxInfo = null;
        return false;
    }
}
