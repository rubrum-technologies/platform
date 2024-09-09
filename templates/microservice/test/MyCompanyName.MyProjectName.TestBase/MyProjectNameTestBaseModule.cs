using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpAutofacModule>]
[DependsOn<AbpTestBaseModule>]
[DependsOn<AbpAuthorizationModule>]
[DependsOn<AbpGuidsModule>]
[DependsOn<MyProjectNameDomainModule>]
public class MyProjectNameTestBaseModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(async () =>
        {
            using var scope = context.ServiceProvider.CreateScope();
            await scope.ServiceProvider
                .GetRequiredService<IDataSeeder>()
                .SeedAsync();
        });
    }
}
