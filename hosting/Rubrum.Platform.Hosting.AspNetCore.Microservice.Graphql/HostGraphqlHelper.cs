using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

#pragma warning disable S2139

public static class HostGraphqlHelper
{
    public static async Task<int> RunServerAsync<TGenerationModule, TModule>(
        string[] args,
        Action<WebApplicationBuilder>? config = null)
        where TGenerationModule : AbpModule
        where TModule : AbpModule
    {
        var assemblyName = typeof(TModule).Assembly.GetName().Name;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", assemblyName)
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            if (args is ["schema", ..])
            {
                return await StartGenerateAsync<TGenerationModule>(args);
            }

            return await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(15, _ => TimeSpan.FromSeconds(15))
                .ExecuteAsync(() => StartAsync<TModule>(args, assemblyName, config));
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{AssemblyName} terminated unexpectedly!", assemblyName);
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    public static async Task<int> StartAsync<TModule>(
        string[] args,
        string? assemblyName,
        Action<WebApplicationBuilder>? config = null)
        where TModule : AbpModule
    {
        Log.Information("Starting {AssemblyName}", assemblyName);

        try
        {
            var app = await ApplicationBuilderHelper.BuildApplicationAsync<TModule>(args, config);

            await app.InitializeApplicationAsync();
            await app.RunWithGraphQLCommandsAsync(args);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{AssemblyName} error start!", assemblyName);
            throw;
        }

        return 0;
    }

    public static async Task<int> StartGenerateAsync<TGenerationModule>(string[] args)
        where TGenerationModule : AbpModule
    {
        Log.Information("Starting generate graphql!");

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host
                .UseAutofac()
                .UseSerilog();

            await builder.AddApplicationAsync<TGenerationModule>();

            var app = builder.Build();

            await app.InitializeApplicationAsync();
            await app.RunWithGraphQLCommandsAsync(args);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Error generate graphql!");
            throw;
        }

        return 0;
    }
}
