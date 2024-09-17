namespace Rubrum.Authorization.Relations;

public interface IRelationValueProviderManager
{
    IReadOnlyList<IRelationValueProvider> ValueProviders { get; }
}
