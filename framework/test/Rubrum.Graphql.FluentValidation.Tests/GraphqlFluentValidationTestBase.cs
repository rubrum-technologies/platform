using Volo.Abp;

namespace Rubrum.Graphql;

public class GraphqlFluentValidationTestBase :
    RubrumGraphqlTestBase<RubrumGraphqlFluentValidationTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
