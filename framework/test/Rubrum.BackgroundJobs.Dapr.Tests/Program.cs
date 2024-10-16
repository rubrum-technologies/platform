using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rubrum.BackgroundJobs;
using Serilog;

var assemblyName = Assembly.GetExecutingAssembly().GetName();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", assemblyName)
    .WriteTo.Async(c => c.Console())
    .CreateLogger();

try
{
    Log.Information("Starting {AssemblyName}", assemblyName);

    var builder = WebApplication.CreateBuilder(args);

    builder.Host
        .UseAutofac()
        .UseSerilog();

    await builder.AddApplicationAsync<RubrumBackgroundJobsDaprTestModule>();

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
