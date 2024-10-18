using Microsoft.Extensions.DependencyInjection;
using Rubrum.Authorization;
using Rubrum.Cqrs;
using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.WindowsService;

[DependsOn<AbpFluentValidationModule>]
[DependsOn<AbpDddApplicationModule>]
[DependsOn<RubrumAuthorizationModule>]
[DependsOn<RubrumGraphqlDddModule>]
[DependsOn<RubrumGraphqlAuthorizationModule>]
[DependsOn<RubrumCqrsModule>]
[DependsOn<WindowsServiceDomainModule>]
public class WindowsServiceApplicationModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddGlobalObjectIdentification()
            .AddMutationConventions()
            .AddFiltering()
            .AddSorting()
            .AddProjections();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddApplicationTypes();
    }
}
