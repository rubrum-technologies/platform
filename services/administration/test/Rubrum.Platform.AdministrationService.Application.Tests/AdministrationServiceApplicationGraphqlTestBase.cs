using Rubrum.Graphql;
using Volo.Abp;

namespace Rubrum.Platform.AdministrationService;

public abstract class AdministrationServiceApplicationGraphqlTestBase :
    RubrumGraphqlTestBase<AdministrationServiceApplicationTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
