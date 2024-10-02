using Authzed.Api.V1;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Rubrum.SpiceDb;

public class RubrumSpiceDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var spiceDbOptions = context.Services.ExecutePreConfiguredActions<RubrumSpiceDbOptions>();

        var credentials = CallCredentials.FromInterceptor((_, metadata) =>
        {
            metadata.Add("Authorization", "Bearer asdasd");  // TODO: Возможность переопределять

            return Task.CompletedTask;
        });

        context.Services
            .AddGrpcClient<PermissionsService.PermissionsServiceClient>(options =>
            {
                spiceDbOptions.PermissionsClient?.ConfigureClient?.Invoke(options);
            })
            .ConfigureChannel(options =>
            {
                options.Credentials = ChannelCredentials.Create(ChannelCredentials.Insecure, credentials);
                spiceDbOptions.PermissionsClient?.ConfigureChannel?.Invoke(options);
            })
            .ConfigureHttpClient(client => { spiceDbOptions.PermissionsClient?.ConfigureHttpClient?.Invoke(client); });

        context.Services
            .AddGrpcClient<SchemaService.SchemaServiceClient>(options =>
            {
                spiceDbOptions.SchemaClient?.ConfigureClient?.Invoke(options);
            })
            .ConfigureChannel(options =>
            {
                options.Credentials = ChannelCredentials.Create(ChannelCredentials.Insecure, credentials);
                spiceDbOptions.SchemaClient?.ConfigureChannel?.Invoke(options);
            })
            .ConfigureHttpClient(client =>
            {
                spiceDbOptions.SchemaClient?.ConfigureHttpClient?.Invoke(client);
            });

        context.Services
            .AddGrpcClient<WatchService.WatchServiceClient>(options =>
            {
                spiceDbOptions.WatchClient?.ConfigureClient?.Invoke(options);
            })
            .ConfigureChannel(options =>
            {
                options.Credentials = ChannelCredentials.Create(ChannelCredentials.Insecure, credentials);
                spiceDbOptions.WatchClient?.ConfigureChannel?.Invoke(options);
            })
            .ConfigureHttpClient(client =>
            {
                spiceDbOptions.WatchClient?.ConfigureHttpClient?.Invoke(client);
            });
    }
}
