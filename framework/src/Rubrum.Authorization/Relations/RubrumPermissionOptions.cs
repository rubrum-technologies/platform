using Volo.Abp.Collections;

namespace Rubrum.Authorization.Relations;

public class RubrumPermissionOptions
{
    public ITypeList<IRelationValueProvider> ValueProviders { get; } = new TypeList<IRelationValueProvider>();
}
