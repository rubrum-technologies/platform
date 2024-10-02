using System.Text;
using HotChocolate.Fusion;
using HotChocolate.Language;
using Volo.Abp.DependencyInjection;
using static Rubrum.Graphql.SpiceDb.WellKnowDirectives;

namespace Rubrum.Graphql.SpiceDb;

public class SpiceDbSchemaBuilder : ISpiceDbSchemaBuilder, ITransientDependency
{
    public async Task<string> BuildAsync(FusionGraphPackage package)
    {
        var configurations = await package.GetSubgraphConfigurationsAsync();
        var schemas = configurations.Select(configuration => Utf8GraphQLParser.Parse(configuration.Schema)).ToList();

        var definitions = CreateDefinitions(schemas);

        // TODO: Добавить возможно переопределять и добовлять станндартные definitions
        definitions.Add(new DefinitionConfiguration("user"));
        definitions.Add(new DefinitionConfiguration("role"));

        return Build(definitions);
    }

    private static string Build(IEnumerable<DefinitionConfiguration> definitions)
    {
        var sb = new StringBuilder();

        foreach (var definition in definitions)
        {
            sb.AppendLine($"definition {definition.Name} {{");

            foreach (var relation in definition.Relations)
            {
                sb.AppendLine($"relation {relation.Name}: {relation.Value}");
            }

            sb.AppendLine();

            foreach (var permission in definition.Permissions)
            {
                sb.AppendLine($"permission {permission.Name} = {permission.Value}");
            }

            sb.AppendLine("}");
        }

        return sb.ToString();
    }

    private static List<DefinitionConfiguration> CreateDefinitions(IEnumerable<DocumentNode> schemas)
    {
        var definitions = new List<DefinitionConfiguration>();

        foreach (var schema in schemas)
        {
            var objectTypes = schema.Definitions.OfType<ObjectTypeDefinitionNode>()
                .Where(x => x.Directives.Any(d => d.Name.Value == Aggregate));

            foreach (var objectType in objectTypes)
            {
                var definition = definitions.SingleOrDefault(d => d.Name == objectType.Name.Value);

                if (definition is null)
                {
                    definition = new DefinitionConfiguration(objectType.Name.Value);
                    definitions.Add(definition);
                }

                foreach (var arguments in objectType.Directives.Where(d => d.Name.Value == Relation)
                             .Select(d => d.Arguments))
                {
                    var name = arguments[0].Value.Value?.ToString();
                    var value = arguments[1].Value.Value?.ToString();

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }

                    definition.AddRelation(name, value);
                }

                foreach (var arguments in objectType.Directives.Where(d => d.Name.Value == Permission)
                             .Select(d => d.Arguments))
                {
                    var name = arguments[0].Value.Value?.ToString();
                    var value = arguments[1].Value.Value?.ToString();

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }

                    definition.AddPermission(name, value);
                }
            }
        }

        return definitions;
    }
}
