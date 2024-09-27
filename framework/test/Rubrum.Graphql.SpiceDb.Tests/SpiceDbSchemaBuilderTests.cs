using CookieCrumble;
using HotChocolate.Fusion;
using Rubrum.Graphql.SpiceDb;
using Shouldly;
using Xunit;

namespace Rubrum.Graphql;

public class SpiceDbSchemaBuilderTests
{
    [Fact]
    public async Task BuildSchema_Gateway()
    {
        var spiceDbSchemaBuilder = new SpiceDbSchemaBuilder();
        await using var package = FusionGraphPackage.Open("./gateway.fgp", FileAccess.Read);

        var spiceDbSchema = await spiceDbSchemaBuilder.BuildAsync(package);

        spiceDbSchema.ShouldNotBeNull();
        spiceDbSchema.MatchMarkdownSnapshot();
    }
}
