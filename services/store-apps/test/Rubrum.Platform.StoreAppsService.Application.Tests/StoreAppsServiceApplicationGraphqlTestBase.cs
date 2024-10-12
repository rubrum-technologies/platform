using Rubrum.Graphql;
using Rubrum.Platform.StoreAppsService;
using Volo.Abp;

namespace Rubrum.Platform.BlobStorageService;

public class StoreAppsServiceApplicationGraphqlTestBase
    : RubrumGraphqlTestBase<StoreAppsServiceApplicationTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
