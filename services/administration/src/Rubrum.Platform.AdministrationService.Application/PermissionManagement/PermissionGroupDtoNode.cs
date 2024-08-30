using HotChocolate.Types;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService.PermissionManagement;

[ObjectType<PermissionGroupDto>]
public static partial class PermissionGroupDtoNode
{
    static partial void Configure(IObjectTypeDescriptor<PermissionGroupDto> descriptor)
    {
        descriptor.Name("PermissionGroup");

        descriptor
            .Field(x => x.Permissions)
            .UseFiltering();
    }
}
