using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Volo.Abp.Modularity;
using Volo.Abp.Reflection;

namespace Rubrum.Cqrs;

[DependsOn<RubrumCqrsContractsModule>]
public class RubrumCqrsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var assemblyFinder = context.Services.GetSingletonInstance<IAssemblyFinder>();

        context.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies([..assemblyFinder.Assemblies]));
    }
}
