using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Rewrite;
using OpenTelemetry.Trace;
using Rubrum.Platform.Gateway.Fusion;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Gateway;

[DependsOn(typeof(PlatformHostingAspNetCoreModule))]
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

    private static void ConfigCors(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddCors(options =>
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
            .AddOpenTelemetry()
            .WithTracing(tracing => { tracing.AddHotChocolateInstrumentation(); });
    }
}
