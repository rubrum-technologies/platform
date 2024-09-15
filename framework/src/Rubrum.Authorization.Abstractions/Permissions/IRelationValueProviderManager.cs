namespace Rubrum.Authorization.Permissions;

public interface IRelationValueProviderManager
{
    IReadOnlyList<IRelationValueProvider> ValueProviders { get; }
}
