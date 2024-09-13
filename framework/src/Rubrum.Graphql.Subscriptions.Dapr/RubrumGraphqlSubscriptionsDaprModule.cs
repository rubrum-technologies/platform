using HotChocolate.Subscriptions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Rubrum.Graphql.Subscriptions;

public class RubrumGraphqlSubscriptionsDaprModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSubscriptionDiagnostics();

        context.Services.AddSingleton<IMessageSerializer, DefaultJsonMessageSerializer>();
    }
}
