using Rubrum.Graphql;
using Rubrum.Modularity;
using Rubrum.Platform.AdministrationService.DbMigrations;
using Rubrum.Platform.AdministrationService.EntityFrameworkCore;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.AdministrationService;

[DependsOn<PlatformHostingAspNetCoreMicroserviceGraphqlModule>]
[DependsOn<AdministrationServiceApplicationModule>]
[DependsOn<AdministrationServiceEntityFrameworkCoreModule>]
public class AdministrationServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, configuration["AuthServer:Audience"]!);

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: [configuration["AuthServer:Audience"]!],
            apiTitle: "AdministrationService API");
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
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "api/administration/swagger/{documentname}/swagger.json";
        });
        app.UseSwaggerUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.RoutePrefix = "api/swagger";
            options.SwaggerEndpoint("/api/administration/swagger/v1/swagger.json", "AdministrationService API");
            options.OAuthClientId(configuration["Swagger:ClientId"]);
        });
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
