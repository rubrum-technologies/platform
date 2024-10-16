﻿using CookieCrumble;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Shouldly;
using Xunit;

namespace Rubrum.Platform.AdministrationService;

public sealed class SchemaTests : AdministrationServiceApplicationGraphqlTestBase
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
