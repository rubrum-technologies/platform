using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Authorization.Permissions;

public class RelationValueProviderManager : IRelationValueProviderManager, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<RubrumPermissionOptions> _options;
    private readonly Lazy<List<IRelationValueProvider>> _lazyProviders;

    public RelationValueProviderManager(
        IServiceProvider serviceProvider,
        IOptions<RubrumPermissionOptions> options)
    {
        _serviceProvider = serviceProvider;
        _options = options;

        _lazyProviders = new Lazy<List<IRelationValueProvider>>(GetProviders, true);
    }

    public IReadOnlyList<IRelationValueProvider> ValueProviders => _lazyProviders.Value;

    protected virtual List<IRelationValueProvider> GetProviders()
    {
        var providers = _options
            .Value
            .ValueProviders
            .Select(type => (_serviceProvider.GetRequiredService(type) as IRelationValueProvider)!)
            .ToList();

        var multipleProviders = providers.GroupBy(p => p.Name).FirstOrDefault(x => x.Count() > 1);
        if (multipleProviders != null)
        {
            throw new AbpException(
                $"Duplicate permission value provider name detected: {multipleProviders.Key}. Providers:{Environment.NewLine}{multipleProviders.Select(p => p.GetType().FullName!).JoinAsString(Environment.NewLine)}");
        }

        return providers;
    }
}
