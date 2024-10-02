using CookieCrumble;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Rubrum.Graphql;
using Shouldly;
using Xunit;

namespace Rubrum.Platform.BlobStorageService;

public sealed class
    SchemaTests : RubrumGraphqlTestBase<PlatformBlobStorageServiceApplicationTestModule>
{
    private readonly IRequestExecutorBuilder _builder;

    public SchemaTests()
    {
        _builder = GetRequiredService<IRequestExecutorBuilder>();
    }

    [Fact]
    public async Task SchemaChangeTest()
    {
        var schema = await _builder.BuildSchemaAsync();

        schema.ShouldNotBeNull();
        schema.MatchSnapshot();
    }
}
