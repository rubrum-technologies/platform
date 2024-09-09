using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Dapr.EventBus;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Caching;
using Volo.Abp.Dapr;
using Volo.Abp.DistributedLocking.Dapr;
using Volo.Abp.EventBus.Dapr;
using Volo.Abp.Http.Client.Dapr;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;

namespace Rubrum.Platform.Hosting;

[DependsOn<AbpHttpClientDaprModule>]
[DependsOn<AbpDistributedLockingDaprModule>]
[DependsOn<AbpEventBusDaprModule>]
[DependsOn<AbpAspNetCoreMvcDaprEventBusModule>]
[DependsOn<AbpBackgroundJobsRabbitMqModule>]
[DependsOn<AbpAspNetCoreAuthenticationJwtBearerModule>]
[DependsOn<AbpHttpClientIdentityModelWebModule>]
[DependsOn<PlatformHostingAspNetCoreModule>]
public class PlatformHostingAspNetCoreMicroserviceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

        var configuration = context.Services.GetConfiguration();

        context.Services.AddDaprClient();

        Configure<AbpDistributedLockDaprOptions>(options => { options.StoreName = "lock-store"; });

        Configure<AbpAspNetCoreMvcOptions>(options => { options.ExposeIntegrationServices = true; });

        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Rubrum:"; });

        Configure<AbpRabbitMqOptions>(options =>
        {
            options.Connections.Default.Uri = new Uri(configuration["ConnectionStrings:broker"]!);
        });

        Configure<AbpDaprOptions>(options =>
        {
            options.HttpEndpoint = configuration["DAPR_HTTP_ENDPOINT"];
            options.GrpcEndpoint = configuration["DAPR_GRPC_ENDPOINT"];
        });
    }
}
