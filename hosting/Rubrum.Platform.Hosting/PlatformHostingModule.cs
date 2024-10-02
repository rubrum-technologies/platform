using Rubrum.Modularity;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Rubrum.Platform.Hosting;

[DependsOn<AbpAutofacModule>]
[DependsOn<AbpTimingModule>]
public class PlatformHostingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }
}
