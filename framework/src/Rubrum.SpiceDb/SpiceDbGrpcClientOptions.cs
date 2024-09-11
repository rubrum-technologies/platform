using Grpc.Net.Client;
using Grpc.Net.ClientFactory;

namespace Rubrum.SpiceDb;

public class SpiceDbGrpcClientOptions
{
    public Action<GrpcClientFactoryOptions>? ConfigureClient { get; set; }

    public Action<GrpcChannelOptions>? ConfigureChannel { get; set; }

    public Action<HttpClient>? ConfigureHttpClient { get; set; }
}
