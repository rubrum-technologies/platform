using Rubrum.Graphql;
using Rubrum.Graphql.SpiceDb;
using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpFluentValidationModule>]
[DependsOn<AbpDddApplicationModule>]
[DependsOn<AbpAutoMapperModule>]
[DependsOn<RubrumGraphqlSpiceDbModule>]
[DependsOn<MyProjectNameApplicationContractsModule>]
[DependsOn<MyProjectNameDomainModule>]
public class MyProjectNameApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MyProjectNameApplicationModule>(validate: true);
        });
    }
}
