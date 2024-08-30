using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Polly;
using Serilog;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

#pragma warning disable S2139

public static class HostHelper
{
    public static async Task<int> RunServerAsync<TModule>(
        string[] args,
        Action<WebApplicationBuilder>? config = null)
        where TModule : AbpModule
    {
        var assemblyName = Assembly.GetAssembly(typeof(TModule))?.GetName().Name;

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

    private static async Task<int> StartAsync<TModule>(
        string[] args,
        string? assemblyName,
        Action<WebApplicationBuilder>? config = null)
        where TModule : AbpModule
    {
        try
        {
            Log.Information("Starting {AssemblyName}", assemblyName);

            var app = await ApplicationBuilderHelper.BuildApplicationAsync<TModule>(args, config);

            await app.InitializeApplicationAsync();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{AssemblyName} error start!", assemblyName);
            throw;
        }

        return 0;
    }
}
