using Rubrum.Graphql;
using Volo.Abp;

namespace Rubrum.Platform.WindowsService;

public class WindowsServiceApplicationGraphqlTestBase
    : RubrumGraphqlTestBase<WindowsServiceApplicationTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
