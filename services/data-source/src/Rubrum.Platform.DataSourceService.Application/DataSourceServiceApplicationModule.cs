using Microsoft.Extensions.DependencyInjection;
using Rubrum.Authorization;
using Rubrum.Cqrs;
using Rubrum.Graphql;
using Rubrum.Modularity;
using Rubrum.Platform.DataSourceService.Database;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Rubrum.Platform.DataSourceService;

[DependsOn<AbpFluentValidationModule>]
[DependsOn<AbpDddApplicationModule>]
[DependsOn<RubrumAuthorizationModule>]
[DependsOn<RubrumGraphqlDddModule>]
[DependsOn<RubrumGraphqlAuthorizationModule>]
[DependsOn<RubrumCqrsModule>]
[DependsOn<DataSourceServiceDomainModule>]
public class DataSourceServiceApplicationModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddGlobalObjectIdentification()
            .AddQueryConventions()
            .AddMutationConventions()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddApplicationTypes()
            .AddTypeModule<DataSourceGraphqlModule>();
    }
}
