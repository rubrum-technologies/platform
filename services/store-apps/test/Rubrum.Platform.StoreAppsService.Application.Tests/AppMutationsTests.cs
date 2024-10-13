using System;
using System.Threading.Tasks;
using CookieCrumble;
using HotChocolate.Types.Relay;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Rubrum.Platform.StoreAppsService.Apps;
using Shouldly;
using Volo.Abp.Users;
using Xunit;
using static Rubrum.Platform.StoreAppsService.AppTestConstants;

namespace Rubrum.Platform.StoreAppsService;

public class AppMutationsTests : StoreAppsServiceApplicationGraphqlTestBase
{
    private readonly INodeIdSerializer _idSerializer;

    private ICurrentUser _currentUser;

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    public AppMutationsTests()
    {
        _idSerializer = GetRequiredService<INodeIdSerializer>();
    }

    [Fact]
    public async Task ActivateApp()
    {
        var appId = _idSerializer.Format(nameof(App), TestAppId);
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
        $$"""
          mutation {
            activateApp(input: {id: "{{appId}}"}) {
                app {
                    enabled
                }
                errors {
                    __typename
                 }
            }
          }
          """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task ChangeNameApp()
    {
        var appId = _idSerializer.Format(nameof(App), TestAppId);
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            $$"""
              mutation {
                changeAppName(input: {
                    id: "{{appId}}"
                    name: "TestChangeName"
                })
                {
                    app {
                        name
                    }
                    errors {
                        __typename
                    }
                }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task CreateApp()
    {
        _currentUser.Id.Returns(Guid.NewGuid());

        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            $$"""
              mutation {
                createApp(input: {
                    enabled: false,
                    name: "CreateAppTest",
                    version: {patch: 122, major: 10, minor: 20}
                }) {
                    app {
                        enabled
                        name
                        version {
                            major
                            minor
                            patch
                         }
                    }
                    errors {
                        __typename
                    }
                }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task DeactivateApp()
    {
        var appId = _idSerializer.Format(nameof(App), TestAppId);
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            $$"""
              mutation {
                deactivateApp(input: {id: "{{appId}}"}) {
                    app {
                        enabled
                    }
                    errors {
                        __typename
                    }
                }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }

    [Fact]
    public async Task DeleteApp()
    {
        var appId = _idSerializer.Format(nameof(App), TestAppId);
        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            $$"""
              mutation {
                deleteApp(input: {id: "{{appId}}"}) {
                    app {
                        name
                    }
                    errors {
                        __typename
                    }
                }
              }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }
}
