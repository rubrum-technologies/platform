using MyCompanyName.MyProjectName.Localization;
using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(AbpDddDomainSharedModule))]
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

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<MyProjectNameResource>("ru")
                .AddVirtualJson("/Localization/MyCompanyName/MyProjectName");

            options.DefaultResourceType = typeof(MyProjectNameResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("MyCompanyName.MyProjectName", typeof(MyProjectNameResource));
        });
    }
}
