using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace Rubrum.Platform.StoreAppsService;

public abstract class StoreAppsServiceTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule>
    where TStartupModule : IAbpModule
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
