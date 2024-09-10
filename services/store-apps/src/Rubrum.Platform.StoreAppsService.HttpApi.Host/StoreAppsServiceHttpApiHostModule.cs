using Rubrum.Platform.StoreAppsService.DbMigrations;
using Rubrum.Platform.StoreAppsService.EntityFrameworkCore;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.StoreAppsService;

[DependsOn(typeof(PlatformHostingAspNetCoreMicroserviceGraphqlModule))]
[DependsOn(typeof(StoreAppsServiceApplicationModule))]
[DependsOn(typeof(StoreAppsServiceHttpApiModule))]
[DependsOn(typeof(StoreAppsServiceEntityFrameworkCoreModule))]
public class StoreAppsServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, configuration["AuthServer:Audience"]!);

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: [configuration["AuthServer:Audience"]!],
            apiTitle: "StoreAppsService API");
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
        app.UseStaticFiles(new StaticFileOptions
        {
            RequestPath = "/api/StoreAppsService/static",
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers.Append(
                    "Cache-Control",
                    $"public, max-age={60 * 60 * 24 * 7}");
            },
        });
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpClaimsMap();
        app.UseAuthorization();
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "api/StoreAppsService/swagger/{documentname}/swagger.json";
        });
        app.UseSwaggerUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.RoutePrefix = "api/swagger";
            options.SwaggerEndpoint("/api/StoreAppsService/swagger/v1/swagger.json", "StoreAppsService API");
            options.OAuthClientId(configuration["Swagger:ClientId"]);
            options.OAuthClientSecret(configuration["Swagger:ClientSecret"]);
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

    public override async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<EfCoreRuntimeDatabaseMigrator>()
            .CheckAndApplyDatabaseMigrationsAsync();
    }
}
