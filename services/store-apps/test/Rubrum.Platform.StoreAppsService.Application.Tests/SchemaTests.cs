using CookieCrumble;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Shouldly;
using Xunit;

namespace Rubrum.Platform.StoreAppsService;

public sealed class SchemaTests : StoreAppsServiceApplicationGraphqlTestBase
{
    private readonly IRequestExecutorBuilder _builder;

    public SchemaTests()
    {
        _builder = GetRequiredService<IRequestExecutorBuilder>();
    }

    [Fact]
    public async Task SchemaChange()
    {
        var schema = await _builder.BuildSchemaAsync();

        schema.ShouldNotBeNull();
        schema.MatchSnapshot();
    }
}
