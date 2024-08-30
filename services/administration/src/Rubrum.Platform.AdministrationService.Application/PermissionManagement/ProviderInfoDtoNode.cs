using HotChocolate.Types;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService.PermissionManagement;

[ObjectType<ProviderInfoDto>]
public static partial class ProviderInfoDtoNode
{
    static partial void Configure(IObjectTypeDescriptor<ProviderInfoDto> descriptor)
    {
        descriptor.Name("PermissionProviderInfo");
    }
}
