using MyCompanyName.MyProjectName.DbMigrations;
using MyCompanyName.MyProjectName.EntityFrameworkCore;
using Rubrum.Modularity;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<PlatformHostingAspNetCoreMicroserviceGraphqlModule>]
[DependsOn<MyProjectNameApplicationModule>]
[DependsOn<MyProjectNameHttpApiModule>]
[DependsOn<MyProjectNameEntityFrameworkCoreModule>]
public class MyProjectNameHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, configuration["AuthServer:Audience"]!);

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: [configuration["AuthServer:Audience"]!],
            apiTitle: "MyProjectName API");
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
            RequestPath = "/api/MyProjectName/static",
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers.Append(
                    "Cache-Control",
                    $"public, max-age={60 * 60 * 24 * 7}");
            },
        });
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwagger(options => { options.RouteTemplate = "api/MyProjectName/swagger/{documentname}/swagger.json"; });
        app.UseSwaggerUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.RoutePrefix = "api/swagger";
            options.SwaggerEndpoint("/api/MyProjectName/swagger/v1/swagger.json", "MyProjectName API");
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
