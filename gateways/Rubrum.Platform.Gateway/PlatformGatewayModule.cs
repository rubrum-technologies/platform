using Authzed.Api.V1;
using HotChocolate.Fusion;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Rewrite;
using OpenTelemetry.Trace;
using Rubrum.Graphql;
using Rubrum.Graphql.SpiceDb;
using Rubrum.Modularity;
using Rubrum.Platform.Gateway.Fusion;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Gateway;

[DependsOn<RubrumGraphqlModule>]
[DependsOn<RubrumGraphqlSpiceDbModule>]
[DependsOn<PlatformHostingAspNetCoreModule>]
public class PlatformGatewayModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigCors(context);
        ConfigYarp(context);
        ConfigFusion(context);

        var configuration = context.Services.GetConfiguration();

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes:
            [
                "administration-service",
                "blob-storage-service",
            ],
            apiTitle: "Rubrum Gateway");
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
        app.UseAbpSerilogEnrichers();
        app.UseCors();
        app.UseHeaderPropagation();
        app.UseSwaggerUiWithYarp();

        app.UseRewriter(new RewriteOptions().AddRedirect(@"^(|\|\s+)$", "/api/swagger"));

        app.UseRouting();
        app.UseRequestTimeouts();
        app.UseWebSockets();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultEndpoints();
            endpoints.MapBananaCakePop("/api/graphql/ui");
            endpoints.MapGraphQL("/api/graphql");
            endpoints.MapReverseProxy();
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        var graphql = context.Services.GetSingletonInstance<FusionGatewayBuilder>();

        graphql.CoreBuilder.InitializeOnStartup();
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await ConfigSpiceDbAsync(context);
    }

    private static void ConfigCors(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services
            .AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]!
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.Trim().RemovePostFix("/"))
                                .ToArray())
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            })
            .AddHeaderPropagation(c =>
            {
                c.Headers.Add("GraphQL-Preflight");
                c.Headers.Add("Authorization");
                c.Headers.Add("Accept-Language");
                c.Headers.Add("Content-Language");
            });
    }

    private static void ConfigYarp(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var proxy = configuration.GetSection("ReverseProxy");

        var yarp = context.Services.AddReverseProxy();

        yarp.LoadFromConfig(proxy);

        context.Services
            .AddSingleton(yarp)
            .AddRequestTimeouts();
    }

    private static void ConfigFusion(ServiceConfigurationContext context)
    {
        var fusion = context.Services
            .AddGraphqlHttpClient()
            .AddFusionGatewayServer()
            .ConfigureFromFile("./gateway.fgp")
            .AddYarpServiceDiscoveryRewriter();

        context.Services.AddSingleton(fusion);

        fusion.CoreBuilder
            .AddType(() => new TimeSpanType(TimeSpanFormat.DotNet))
            .AddInstrumentation();

        context.Services
            .AddHttpClient("http")
            .AddHeaderPropagation();

        context.Services
            .AddOpenTelemetry()
            .WithTracing(tracing => { tracing.AddHotChocolateInstrumentation(); });
    }

    private static async Task ConfigSpiceDbAsync(ApplicationInitializationContext context)
    {
        var spiceDbSchemaBuilder = context.ServiceProvider.GetRequiredService<ISpiceDbSchemaBuilder>();
        var spiceDbSchemaClient = context.ServiceProvider.GetRequiredService<SchemaService.SchemaServiceClient>();
        await using var package = FusionGraphPackage.Open("./gateway.fgp", FileAccess.Read);

        var spiceDbSchema = await spiceDbSchemaBuilder.BuildAsync(package);

        await spiceDbSchemaClient.WriteSchemaAsync(new WriteSchemaRequest { Schema = spiceDbSchema });
    }
}
