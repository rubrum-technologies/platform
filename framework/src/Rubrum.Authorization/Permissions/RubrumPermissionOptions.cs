using Volo.Abp.Collections;

namespace Rubrum.Authorization.Permissions;

public class RubrumPermissionOptions
{
    public ITypeList<IRelationValueProvider> ValueProviders { get; } = new TypeList<IRelationValueProvider>();
}
