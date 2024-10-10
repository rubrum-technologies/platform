using Microsoft.Extensions.DependencyInjection;
using Rubrum.Authorization;
using Rubrum.Cqrs;
using Rubrum.Graphql;
using Rubrum.Modularity;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService;

[DependsOn<AbpFluentValidationModule>]
[DependsOn<AbpPermissionManagementApplicationModule>]
[DependsOn<RubrumAuthorizationModule>]
[DependsOn<RubrumGraphqlDddModule>]
[DependsOn<RubrumGraphqlAuthorizationModule>]
[DependsOn<RubrumCqrsModule>]
[DependsOn<AdministrationServiceDomainModule>]
public class AdministrationServiceApplicationModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.GetGraphql()
            .AddQueryConventions()
            .AddMutationConventions()
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .AddApplicationTypes();
    }
}
