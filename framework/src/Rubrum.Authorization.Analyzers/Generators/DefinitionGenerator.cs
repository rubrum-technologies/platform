using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Rubrum.Analyzers.Generators;
using Rubrum.Analyzers.Models;
using Rubrum.Authorization.Analyzers.FileBuilders;
using Rubrum.Authorization.Analyzers.Models;

namespace Rubrum.Authorization.Analyzers.Generators;

public class DefinitionGenerator : ISyntaxGenerator
{
    public void Generate(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<SyntaxInfo> syntaxInfos)
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
                builder.WriteRelationProperty(relation.ClassName, relation.PropertyName);
            }

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
        ImmutableArray<SyntaxInfo> syntaxInfos)
    {
        var builder = new RelationFileBuilder();
        var definitions = syntaxInfos
            .OfType<DefinitionInfo>()
            .ToList();
        var relations = definitions
            .SelectMany(d => d.Relations)
            .ToList();

        builder.WriteBeginClass(relation);

        foreach (var fullName in relation.Classes)
        {
            var isRelation = relations.Exists(r => r.FullName == fullName);

            if (isRelation)
            {
                var relationLink = relations.Single(r => r.FullName == fullName);

                foreach (var definition in definitions.Where(d => relationLink.Classes.Contains(d.FullName)))
                {
                    CreateRelationProperty(builder, fullName, definition);
                }
            }
            else
            {
                var definition = definitions.Single(d => d.FullName == fullName);

                CreateRelationProperty(builder, fullName, definition);
            }
        }

        builder.WriteEndClass();

        return builder.ToSourceText();
    }

    private static void CreateRelationProperty(
        RelationFileBuilder builder,
        string fullName,
        DefinitionInfo definition)
    {
        foreach (var propertyName in definition.Relations.Select(r => r.PropertyName))
        {
            builder.WriteProperty(propertyName, fullName);
        }

        foreach (var propertyName in definition.Permissions.Select(r => r.PropertyName))
        {
            builder.WriteProperty(propertyName, fullName);
        }
    }
}
