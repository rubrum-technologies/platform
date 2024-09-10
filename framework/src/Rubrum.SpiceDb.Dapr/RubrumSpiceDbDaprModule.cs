using Authzed.Api.V1;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Rubrum.SpiceDb;

[DependsOn<RubrumSpiceDbModule>]
public class RubrumSpiceDbDaprModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services
            .AddGrpcClient<PermissionsService.PermissionsServiceClient>(options =>
            {
                options.Address = new Uri(configuration["Test"]!);
            })
            .ConfigureChannel(options => { options.UnsafeUseInsecureChannelCallCredentials = true; })
            .ConfigureHttpClient(client => { client.DefaultRequestHeaders.Add("dapr-app-id", "spice-db-service"); });
    }
}
