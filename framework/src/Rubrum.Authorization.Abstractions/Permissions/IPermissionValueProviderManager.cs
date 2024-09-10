namespace Rubrum.Authorization.Permissions;

public interface IPermissionValueProviderManager
{
    IReadOnlyList<IPermissionValueProvider> ValueProviders { get; }
}
