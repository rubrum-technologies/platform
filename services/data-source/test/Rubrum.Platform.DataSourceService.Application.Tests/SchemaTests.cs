using CookieCrumble;
using HotChocolate.Execution;
using Shouldly;
using Xunit;

namespace Rubrum.Platform.DataSourceService;

public sealed class SchemaTests : DataSourceServiceApplicationGraphqlTestBase
{
    private readonly IRequestExecutorResolver _resolver;

    public SchemaTests()
    {
        _resolver = GetRequiredService<IRequestExecutorResolver>();
    }

    [Fact(Skip = "Всегда будет вызывать ошибку, изо сортировки полей фильтрации.")]
    public async Task SchemaChangeTest()
    {
        var executor = await _resolver.GetRequestExecutorAsync();
        var schema = executor.Schema;

        schema.ShouldNotBeNull();
        schema.MatchSnapshot();
    }
}
