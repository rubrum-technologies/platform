using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(MyProjectNameDomainSharedModule))]
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
