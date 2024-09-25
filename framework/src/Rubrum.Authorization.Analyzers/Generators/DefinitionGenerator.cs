using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Rubrum.Authorization.Analyzers.FileBuilders;
using Rubrum.Authorization.Analyzers.Models;
using static Rubrum.Authorization.Analyzers.WellKnownClasses;

namespace Rubrum.Authorization.Analyzers.Generators;

public class DefinitionGenerator : ISyntaxGenerator
{
    public void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<ISyntaxInfo> syntaxInfos)
    {
        var definitions = syntaxInfos.OfType<DefinitionInfo>().ToList();

        foreach (var definition in definitions)
        {
            using var builder = new DefinitionFileBuilder();

            builder.WriteHeader();

            builder.WriteBeginNamespace(definition.Namespace);

            builder.WriteBeginClass(definition.ClassName);

            foreach (var relation in definition.Relations)
            {
                builder.WriteRelationProperty(relation);
            }

            foreach (var permission in definition.Permissions)
            {
                builder.WritePermissionProperty(permission);
            }

            foreach (var permission in definition.Permissions)
            {
                builder.WritePermissionMethod(permission);
            }

            builder.WritePermissionsClass(definition);

            foreach (var relation in definition.Relations)
            {
                builder.WriteRelationClass(CreateRelationClass(relation, syntaxInfos));
            }

            builder.WriteEndClass();

            builder.WriteEndNamespace();

            context.AddSource($"{definition.ClassName}.g.cs", builder.ToSourceText());
        }
    }

    private static SourceText CreateRelationClass(
        RelationInfo relation,
        ImmutableArray<ISyntaxInfo> syntaxInfos)
    {
        var builder = new RelationFileBuilder();
        var definitions = syntaxInfos
            .OfType<DefinitionInfo>()
            .ToList();

        builder.WriteBeginClass(relation);

        foreach (var value in relation.Values)
        {
            var definition = definitions
                .SingleOrDefault(d => d.TypeSymbol.Equals(value, SymbolEqualityComparer.Default));

            if (definition is not null)
            {
                foreach (var propertyName in definition.Relations.Select(p => p.PropertyName))
                {
                    builder.WriteProperty(propertyName, value.ToDisplayString());
                }

                foreach (var propertyName in definition.Permissions.Select(p => p.PropertyName))
                {
                    builder.WriteProperty(propertyName, value.ToDisplayString());
                }
            }

            var properties = value.GetMembers()
                .OfType<IPropertySymbol>()
                .Where(p =>
                    p.Type.BaseType?.ToDisplayString() == RelationClass ||
                    p.Type.ToDisplayString() == PermissionLinkClass)
                .ToList();

            foreach (var property in properties)
            {
                builder.WriteProperty(property.Name, value.ToDisplayString());
            }
        }

        builder.WriteEndClass();

        return builder.ToSourceText();
    }
}
