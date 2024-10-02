using Microsoft.Extensions.DependencyInjection;
using Rubrum.Modularity;
using Testcontainers.PostgreSql;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Rubrum.TestContainers.PostgreSql;

[DependsOn<AbpTestBaseModule>]
public class RubrumTestContainersPostgreSqlModule : AbpModule
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder().Build();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton(_container);
    }

    public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(() => _container.StartAsync());
    }

    public override void OnApplicationShutdown(ApplicationShutdownContext context)
    {
        AsyncHelper.RunSync(() => _container.DisposeAsync().AsTask());
    }
}
