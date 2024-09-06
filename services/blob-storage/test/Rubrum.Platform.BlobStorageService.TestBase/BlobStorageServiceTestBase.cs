using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace Rubrum.Platform.BlobStorageService;

public abstract class BlobStorageServiceTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule>
    where TStartupModule : IAbpModule
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
