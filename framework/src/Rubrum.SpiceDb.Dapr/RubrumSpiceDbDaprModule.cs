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
            options.Address = new Uri(configuration["Test"]!);
        };

        Action<GrpcChannelOptions> configureChannel = options =>
        {
            options.UnsafeUseInsecureChannelCallCredentials = true;
        };

        Action<HttpClient> configureHttpClient = options =>
        {
            options.DefaultRequestHeaders.Add("dapr-app-id", "spice-db-service");
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
