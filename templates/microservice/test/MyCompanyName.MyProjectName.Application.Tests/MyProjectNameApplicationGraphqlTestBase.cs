using Rubrum.Graphql;
using Volo.Abp;

namespace MyCompanyName.MyProjectName;

public abstract class MyProjectNameApplicationGraphqlTestBase
    : RubrumGraphqlTestBase<MyProjectNameApplicationTestModule>
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}
