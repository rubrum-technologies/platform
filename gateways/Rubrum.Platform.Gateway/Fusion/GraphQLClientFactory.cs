using System.Reflection;
using HotChocolate.Fusion.Clients;
using HotChocolate.Fusion.Metadata;

namespace Rubrum.Platform.Gateway.Fusion;

internal sealed class GraphQlClientFactory(
    IHttpClientFactory httpClientFactory) : IGraphQLClientFactory
{
    private static readonly ConstructorInfo DefaultHttpGraphQlClientConstructor = AppDomain.CurrentDomain
        .GetAssemblies()
        .SelectMany(x => x.GetTypes())
        .Single(x => x.Name == "DefaultHttpGraphQLClient")
        .GetConstructors()
        .First(x => x.GetParameters().Length == 2);

    public IGraphQLClient CreateClient(HttpClientConfiguration configuration)
    {
        return (IGraphQLClient)DefaultHttpGraphQlClientConstructor.Invoke([
            configuration,
            CreateHttpClient(configuration),
        ]);
    }

    private HttpClient CreateHttpClient(HttpClientConfiguration configuration)
    {
        var client = httpClientFactory.CreateClient(configuration.ClientName);

        client.DefaultRequestHeaders.Add("dapr-app-id", configuration.SubgraphName);

        return client;
    }
}
