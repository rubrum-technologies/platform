using Volo.Abp.Collections;

namespace Rubrum.Authorization.Permissions;

public class RubrumPermissionOptions
{
    public ITypeList<IPermissionValueProvider> ValueProviders { get; } = new TypeList<IPermissionValueProvider>();
}
