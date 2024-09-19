using Rubrum.Graphql.SpiceDb;
using Rubrum.Modularity;
using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn<AbpFluentValidationModule>]
[DependsOn<AbpDddApplicationModule>]
[DependsOn<RubrumGraphqlSpiceDbModule>]
[DependsOn<MyProjectNameApplicationContractsModule>]
[DependsOn<MyProjectNameDomainModule>]
public class MyProjectNameApplicationModule : AbpModule;
