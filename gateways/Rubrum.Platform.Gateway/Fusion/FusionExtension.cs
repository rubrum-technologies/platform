using HotChocolate.Fusion.Clients;
using HotChocolate.Fusion.Metadata;

namespace Rubrum.Platform.Gateway.Fusion;

public static class FusionExtension
{
    public static FusionGatewayBuilder AddYarpServiceDiscoveryRewriter(
        this FusionGatewayBuilder builder)
    {
        builder.Services.AddSingleton<IConfigurationRewriter, YarpServiceDiscoveryConfigurationRewriter>();
        return builder;
    }

    public static IServiceCollection AddGraphqlHttpClient(this IServiceCollection services)
    {
        return services.AddSingleton<IGraphQLClientFactory, GraphQlClientFactory>();
    }
}
