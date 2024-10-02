using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

public static class ApplicationBuilderHelper
{
    public static async Task<WebApplication> BuildApplicationAsync<TModule>(
        string[] args,
        Action<WebApplicationBuilder>? config = null)
        where TModule : IAbpModule
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host
            .AddAppSettingsSecretsJson()
            .UseAutofac()
            .UseSerilog((ctx, logger) =>
            {
                logger
                    .Enrich.FromLogContext()
#if DEBUG
                    .MinimumLevel.Debug()
#else
                    .MinimumLevel.Information()
#endif
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                    .Enrich.WithProperty("Application", typeof(TModule).Assembly.GetName().Name)
                    .WriteTo.Async(c => c.File("Logs/logs.txt"))
                    .WriteTo.Async(c => c.Console())
                    .WriteTo.OpenTelemetry(options =>
                    {
                        var endpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];

                        if (string.IsNullOrWhiteSpace(endpoint))
                        {
                            return;
                        }

                        options.Endpoint = endpoint;

                        options.ResourceAttributes.Add(
                            "service.name",
                            ctx.HostingEnvironment.ApplicationName.ToLower().Replace(".", "-"));

                        var headers = builder.Configuration["OTEL_EXPORTER_OTLP_HEADERS"]?.Split(',') ?? [];

                        foreach (var header in headers)
                        {
                            var (key, value) = header.Split('=') switch
                            {
                                [var k, var v] => (k, v),
                                var v => throw new AbpException($"Invalid header format {v}"),
                            };

                            options.Headers.Add(key, value);
                        }

                        var attributes = builder.Configuration["OTEL_RESOURCE_ATTRIBUTES"]?.Split(',') ?? [];

                        foreach (var attribute in attributes)
                        {
                            var (key, value) = attribute.Split("=") switch
                            {
                                [var k, var v] => (k, v),
                                var v => throw new AbpException($"Invalid header format {v}"),
                            };

                            options.ResourceAttributes.Add(key, value);
                        }
                    })
                    .ReadFrom.Configuration(ctx.Configuration);
            });

        config?.Invoke(builder);

        await builder.AddApplicationAsync<TModule>();

        return builder.Build();
    }
}
