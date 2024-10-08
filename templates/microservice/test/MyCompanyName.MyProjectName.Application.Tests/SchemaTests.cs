﻿using CookieCrumble;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Rubrum.Graphql;
using Shouldly;

namespace MyCompanyName.MyProjectName;

public sealed class SchemaTests : RubrumGraphqlTestBase<MyProjectNameApplicationTestModule>
{
    private readonly IRequestExecutorBuilder _builder;

    public SchemaTests()
    {
        _builder = GetRequiredService<IRequestExecutorBuilder>();
    }

    //[Fact]
    public async Task SchemaChangeTest()
    {
        var schema = await _builder.BuildSchemaAsync();

        schema.ShouldNotBeNull();
        schema.MatchSnapshot();
    }
}
