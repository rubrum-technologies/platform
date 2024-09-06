using Rubrum.Platform.AdministrationService.DbMigrations;
using Rubrum.Platform.AdministrationService.EntityFrameworkCore;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Rubrum.Platform.AdministrationService;

[DependsOn(typeof(PlatformHostingAspNetCoreMicroserviceGraphqlModule))]
[DependsOn(typeof(PlatformAdministrationServiceApplicationModule))]
[DependsOn(typeof(PlatformAdministrationServiceHttpApiModule))]
[DependsOn(typeof(PlatformAdministrationServiceEntityFrameworkCoreModule))]
public class PlatformAdministrationServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, "administration-service");

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: [configuration["AuthServer:Audience"]!],
            apiTitle: "AdministrationService API");

        Configure<FeatureManagementOptions>(options => { options.IsDynamicFeatureStoreEnabled = true; });

        Configure<SettingManagementOptions>(options => { options.IsDynamicSettingStoreEnabled = true; });

        Configure<PermissionManagementOptions>(options => { options.IsDynamicPermissionStoreEnabled = true; });
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
            RequestPath = "/api/administration/static",
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

    public override async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<EfCoreRuntimeDatabaseMigrator>()
            .CheckAndApplyDatabaseMigrationsAsync();
    }
}
