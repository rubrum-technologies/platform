using Rubrum.Modularity;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

[DependsOn<AbpAutofacModule>]
public class PlatformHostingModule : AbpModule;
