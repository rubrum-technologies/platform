using HotChocolate.Fusion.Metadata;

namespace Rubrum.Platform.Gateway.Fusion;

public class YarpServiceDiscoveryConfigurationRewriter(IConfiguration netConfiguration) : ConfigurationRewriter
{
    protected override ValueTask<HttpClientConfiguration> RewriteAsync(
        HttpClientConfiguration configuration,
        CancellationToken cancellationToken)
    {
        var x = configuration with
        {
            EndpointUri = new Uri($"{netConfiguration["ReverseProxy:Clusters:dapr-cluster:Destinations:Default:Address"]}/graphql"),
        };

        return new ValueTask<HttpClientConfiguration>(x);
    }
}
