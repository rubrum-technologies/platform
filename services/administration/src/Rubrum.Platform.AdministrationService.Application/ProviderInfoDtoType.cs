using HotChocolate.Types;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService;

[ObjectType<ProviderInfoDto>]
public static partial class ProviderInfoDtoType
{
    static partial void Configure(IObjectTypeDescriptor<ProviderInfoDto> descriptor)
    {
        descriptor.Name("PermissionProviderInfo");
    }
}
