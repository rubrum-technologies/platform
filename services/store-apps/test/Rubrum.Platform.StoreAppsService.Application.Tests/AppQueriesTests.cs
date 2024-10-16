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
    public async Task GetAppById()
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

    [Fact]
    public async Task GetAppById_EntityNotFound()
    {
        var appId = _idSerializer.Format(nameof(App), Guid.NewGuid());

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

    [Fact]
    public async Task GetApps()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
              """
              query {
                  apps {
                     nodes {
                        name
                        enabled
                        version {
                            major
                            minor
                            patch
                        }
                        ownerId
                        tenantId
                     }
                  }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task GetApps_FirstOne()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
              """
              query {
                  apps(first: 1) {
                     nodes {
                        name
                        enabled
                        version {
                            major
                            minor
                            patch
                        }
                        ownerId
                        tenantId
                     }
                  }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task GetApps_LastOne()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
              """
              query {
                  apps(last: 1) {
                     nodes {
                        name
                        enabled
                        version {
                            major
                            minor
                            patch
                        }
                        ownerId
                        tenantId
                     }
                  }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task GetApps_FilterByMinorVersionNeqZero()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
              """
              query {
                  apps(where: {version: {minor: {neq: 0}}}) {
                     nodes {
                        name
                        enabled
                        version {
                            major
                            minor
                            patch
                        }
                        ownerId
                        tenantId
                     }
                  }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task GetApps_FilterByMinorVersionEqZero()
    {
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
              """
              query {
                  apps(where: {version: {minor: {eq: 0}}}) {
                     nodes {
                        name
                        enabled
                        version {
                            major
                            minor
                            patch
                        }
                        ownerId
                        tenantId
                     }
                  }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }
}
