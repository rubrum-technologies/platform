using Rubrum.Generate;
using Rubrum.Modularity;
using Rubrum.PermissionManagement;
using Volo.Abp.Modularity;

namespace Generate;

[DependsOn<RubrumGenerateClientProxiesModule>]
[DependsOn<RubrumPermissionManagementHttpApiModule>]
public class GenerateModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var projects = new Dictionary<string, string>
        {
            {
                RubrumPermissionManagementRemoteServiceConstants.ModuleName,
                "modules/permission-management/src/Rubrum.PermissionManagement.HttpApi.Client"
            },
        };

        Configure<RubrumGenerateClientProxiesOptions>(options =>
        {
            foreach (var (key, value) in projects)
            {
                options.ProxyClients.Add(key, value);
            }
        });
    }
}
