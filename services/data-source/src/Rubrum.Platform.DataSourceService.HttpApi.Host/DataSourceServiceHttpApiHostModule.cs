using Rubrum.Graphql;
using Rubrum.Platform.DataSourceService.DbMigrations;
using Rubrum.Platform.DataSourceService.EntityFrameworkCore;
using Rubrum.Modularity;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.DataSourceService;

[DependsOn<PlatformHostingAspNetCoreMicroserviceGraphqlModule>]
[DependsOn<DataSourceServiceApplicationModule>]
[DependsOn<DataSourceServiceEntityFrameworkCoreModule>]
public class DataSourceServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, configuration["AuthServer:Audience"]!);
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        context.Services
            .GetGraphql()
            .InitializeOnStartup();
    }

    public override async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<EfCoreRuntimeDatabaseMigrator>()
            .CheckAndApplyDatabaseMigrationsAsync();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();
        app.UseAbpRequestLocalization();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseWebSockets();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapDefaultEndpoints();
            endpoints.MapGraphQL();
        });
    }
}
