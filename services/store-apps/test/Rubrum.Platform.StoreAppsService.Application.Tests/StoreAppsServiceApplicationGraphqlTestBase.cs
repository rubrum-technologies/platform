using Rubrum.Graphql;
using Volo.Abp;

namespace Rubrum.Platform.StoreAppsService;

public class StoreAppsServiceApplicationGraphqlTestBase
    : RubrumGraphqlTestBase<StoreAppsServiceApplicationTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
