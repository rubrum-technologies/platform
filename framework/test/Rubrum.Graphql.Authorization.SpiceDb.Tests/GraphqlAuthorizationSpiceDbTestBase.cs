using Rubrum.Graphql;
using Volo.Abp;

namespace Rubrum.Authorization.Analyzers;

public class GraphqlAuthorizationSpiceDbTestBase : RubrumGraphqlTestBase<RubrumGraphqlAuthorizationSpiceDbTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
