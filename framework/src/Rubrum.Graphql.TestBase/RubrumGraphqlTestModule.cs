using System.Globalization;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql;

[DependsOn(typeof(AbpTestBaseModule))]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(RubrumGraphqlModule))]
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
