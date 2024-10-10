using HotChocolate.Types;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService;

[ObjectType<PermissionGrantInfoDto>]
public static partial class PermissionGrantInfoDtoType
{
    static partial void Configure(IObjectTypeDescriptor<PermissionGrantInfoDto> descriptor)
    {
        descriptor.Name("PermissionGrantInfo");
    }
}
