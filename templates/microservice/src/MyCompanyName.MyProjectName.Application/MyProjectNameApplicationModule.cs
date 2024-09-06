using Rubrum.Graphql;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(AbpFluentValidationModule))]
[DependsOn(typeof(AbpDddApplicationModule))]
[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(RubrumGraphqlModule))]
[DependsOn(typeof(MyProjectNameApplicationContractsModule))]
[DependsOn(typeof(MyProjectNameDomainModule))]
public class MyProjectNameApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //var graphql = context.Services.GetGraphql();
        //graphql.AddApplicationTypes();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MyProjectNameApplicationModule>(validate: true);
        });
    }
}
