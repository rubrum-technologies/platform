using Rubrum.Generate;
using Rubrum.Modularity;
using Volo.Abp.Modularity;

namespace Generate;

[DependsOn<RubrumGenerateClientProxiesModule>]
public class GenerateModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var projects = new Dictionary<string, string>
        {
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
