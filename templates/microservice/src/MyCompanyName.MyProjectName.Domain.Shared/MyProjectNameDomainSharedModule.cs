using Rubrum.Auditing;
using Rubrum.Modularity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpDddDomainSharedModule>]
[DependsOn<RubrumAuditingContractsModule>]
public class MyProjectNameDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MyProjectNameGlobalFeatureConfigurator.Configure();
        MyProjectNameModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MyProjectNameDomainSharedModule>();
        });
    }
}
