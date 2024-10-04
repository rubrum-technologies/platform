using Rubrum.Graphql;
using Volo.Abp;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceServiceApplicationGraphqlTestBase
    : RubrumGraphqlTestBase<DataSourceServiceApplicationTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
