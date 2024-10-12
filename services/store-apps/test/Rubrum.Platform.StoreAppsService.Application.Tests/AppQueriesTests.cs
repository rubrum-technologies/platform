using CookieCrumble;
using HotChocolate.Types.Relay;
using Rubrum.Platform.StoreAppsService.Apps;
using Shouldly;
using Xunit;
using static Rubrum.Platform.StoreAppsService.AppTestConstants;

namespace Rubrum.Platform.StoreAppsService;

public class AppQueriesTests : StoreAppsServiceApplicationGraphqlTestBase
{
    private readonly INodeIdSerializer _idSerializer;

    public AppQueriesTests()
    {
        _idSerializer = GetRequiredService<INodeIdSerializer>();
    }

    [Fact]
    public async Task GetAppByIdTest()
    {
        var appId = _idSerializer.Format(nameof(App), TestAppId);

        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
        $$"""
          query {
              appById(id: "{{appId}}") {
                  name
                  ownerId
                  version {
                    major
                    minor
                    patch
                  }
                  enabled
              }
          }
          """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

}
