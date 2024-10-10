using HotChocolate.Types;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService;

[ObjectType<PermissionGroupDto>]
public static partial class PermissionGroupDtoType
{
    static partial void Configure(IObjectTypeDescriptor<PermissionGroupDto> descriptor)
    {
        descriptor.Name("PermissionGroupDto");
    }
}
