using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

#pragma warning disable S2139

public static partial class HostGraphqlHelper
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
            return await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, i => TimeSpan.FromSeconds(15 * i))
                .ExecuteAsync(() => StartAsync<TGenerationModule, TModule>(args, assemblyName, config));
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

    public static async Task<int> StartAsync<TGenerationModule, TModule>(
        string[] args,
        string? assemblyName,
        Action<WebApplicationBuilder>? config = null)
        where TGenerationModule : AbpModule
        where TModule : AbpModule
    {
        Log.Information("Starting {AssemblyName}", assemblyName);

        try
        {
            WebApplication app;

            if (args is ["schema", ..])
            {
                app = await ApplicationBuilderHelper.BuildApplicationAsync<TGenerationModule>(args, config);
            }
            else
            {
                app = await ApplicationBuilderHelper.BuildApplicationAsync<TModule>(args, config);
            }

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
}
