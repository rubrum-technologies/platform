using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Volo.Abp.Modularity;

namespace Rubrum.Generate;

public static class GenerateHelperRun
{
    public static async Task<int> RunAsync<TModule>(string[] args)
        where TModule : AbpModule
    {
        var assemblyName = typeof(TModule).Assembly.GetName().Name;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", $"{assemblyName}")
            .WriteTo.Async(c => c.Console())
            .CreateLogger();
        try
        {
            Log.Information("Starting {AssemblyName}", assemblyName);

            var builder = WebApplication.CreateBuilder(args);

            builder.Host
                .UseAutofac()
                .UseSerilog();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenLocalhost(Random.Shared.Next(10000, 30000));
            });

            await builder.AddApplicationAsync<TModule>();

            var app = builder.Build();

            await app.InitializeApplicationAsync();
            await app.RunAsync();

            return 0;
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
}
