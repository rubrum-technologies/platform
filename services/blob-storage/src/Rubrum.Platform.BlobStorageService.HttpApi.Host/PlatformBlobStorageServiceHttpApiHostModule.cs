using Rubrum.Graphql.SpiceDb;
using Rubrum.Modularity;
using Rubrum.Platform.BlobStorageService.Blobs;
using Rubrum.Platform.BlobStorageService.DbMigrations;
using Rubrum.Platform.BlobStorageService.EntityFrameworkCore;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.BlobStorageService;

[DependsOn<PlatformHostingAspNetCoreMicroserviceGraphqlModule>]
[DependsOn<PlatformBlobStorageServiceApplicationModule>]
[DependsOn<PlatformBlobStorageServiceHttpApiModule>]
[DependsOn<PlatformBlobStorageServiceEntityFrameworkCoreModule>]
public class PlatformBlobStorageServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, configuration["AuthServer:Audience"]!);

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: [configuration["AuthServer:Audience"]!],
            apiTitle: "BlobStorageService API");
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
            options.RouteTemplate = "api/blob-storage/swagger/{documentname}/swagger.json";
        });
        app.UseSwaggerUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.RoutePrefix = "api/swagger";
            options.SwaggerEndpoint("/api/blob-storage/swagger/v1/swagger.json", "BlobStorageService API");
            options.OAuthClientId(configuration["Swagger:ClientId"]);
            options.OAuthClientSecret(configuration["Swagger:ClientSecret"]);
        });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseWebSockets();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<BlobGrpcService>();

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
