using HotChocolate.Types;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService.PermissionManagement;

[ObjectType<PermissionGrantInfoDto>]
public static partial class PermissionGrantInfoDtoNode
{
    static partial void Configure(IObjectTypeDescriptor<PermissionGrantInfoDto> descriptor)
    {
        descriptor.Name("PermissionGrantInfo");

        descriptor
            .Field(x => x.AllowedProviders)
            .UseFiltering();

        descriptor
            .Field(x => x.GrantedProviders)
            .UseFiltering();
    }
}
