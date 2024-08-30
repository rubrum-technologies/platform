using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rubrum.Platform.Hosting;

[DependsOn(typeof(AbpAutofacModule))]
public class PlatformHostingModule : AbpModule;
