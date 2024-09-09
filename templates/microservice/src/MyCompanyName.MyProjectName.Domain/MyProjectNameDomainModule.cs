using Rubrum.Modularity;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpAutoMapperModule>]
[DependsOn<AbpDddDomainModule>]
[DependsOn<MyProjectNameDomainSharedModule>]
public class MyProjectNameDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MyProjectNameDomainModule>(validate: true);
        });
    }
}
