using HotChocolate.Fusion;

namespace Rubrum.Graphql.SpiceDb;

public interface ISpiceDbSchemaBuilder
{
    Task<string> BuildAsync(FusionGraphPackage package);
}
