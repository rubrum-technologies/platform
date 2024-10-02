using Volo.Abp.Modularity;

namespace Rubrum.Modularity;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute<TModule>() : DependsOnAttribute(typeof(TModule))
    where TModule : AbpModule;
