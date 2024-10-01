using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rubrum.Modularity;
using Rubrum.Platform.Hosting;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Rubrum.BackgroundJobs;

[DependsOn<PlatformHostingAspNetCoreModule>]
[DependsOn<RubrumBackgroundJobsDaprModule>]
public class RubrumBackgroundJobsDaprTestModule : AbpModule
{
    public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var logger = context.ServiceProvider.GetRequiredService<ILogger<RubrumBackgroundJobsDaprTestModule>>();
        var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
        logger.LogInformation($"MySettingName => {configuration["MySettingName"]}");

        var hostEnvironment = context.ServiceProvider.GetRequiredService<IHostEnvironment>();
        logger.LogInformation($"EnvironmentName => {hostEnvironment.EnvironmentName}");

        return Task.CompletedTask;
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var job = context.ServiceProvider.GetRequiredService<IBackgroundJobManager>();

        var result = await job.EnqueueAsync(new MyAsyncJobArgs("42"));
    }
}
