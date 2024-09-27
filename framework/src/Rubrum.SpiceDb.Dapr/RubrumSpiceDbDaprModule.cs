using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.SpiceDb;

[DependsOn<RubrumSpiceDbModule>]
public class RubrumSpiceDbDaprModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Action<GrpcClientFactoryOptions> configureClient = options =>
        {
            options.Address = new Uri(configuration["DAPR_GRPC_ENDPOINT"]!); // TODO: Возможность переопределять
        };

        Action<GrpcChannelOptions> configureChannel = options =>
        {
            options.UnsafeUseInsecureChannelCallCredentials = true;
        };

        Action<HttpClient> configureHttpClient = options =>
        {
            options.DefaultRequestHeaders.Add("dapr-app-id", "spicedb-service"); // TODO: Возможность переопределять
        };

        PreConfigure<RubrumSpiceDbOptions>(spiceDbOptions =>
        {
            spiceDbOptions.PermissionsClient = new SpiceDbGrpcClientOptions
            {
                ConfigureClient = configureClient,
                ConfigureChannel = configureChannel,
                ConfigureHttpClient = configureHttpClient,
            };

            spiceDbOptions.SchemaClient = new SpiceDbGrpcClientOptions
            {
                ConfigureClient = configureClient,
                ConfigureChannel = configureChannel,
                ConfigureHttpClient = configureHttpClient,
            };

            spiceDbOptions.WatchClient = new SpiceDbGrpcClientOptions
            {
                ConfigureClient = configureClient,
                ConfigureChannel = configureChannel,
                ConfigureHttpClient = configureHttpClient,
            };
        });
    }
}
