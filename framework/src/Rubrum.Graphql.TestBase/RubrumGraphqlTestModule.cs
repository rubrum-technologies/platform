using System.Globalization;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn<AbpTestBaseModule>]
[DependsOn<AbpAutofacModule>]
[DependsOn<RubrumGraphqlAuthorizationModule>]
public class RubrumGraphqlTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        context.Services.AddSingleton(sp =>
            new RequestExecutorProxy(sp.GetRequiredService<IRequestExecutorResolver>(), "_Default"));
    }
}
