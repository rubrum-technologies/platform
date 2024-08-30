using HotChocolate;
using HotChocolate.Types;
using Volo.Abp.PermissionManagement;

namespace Rubrum.Platform.AdministrationService.PermissionManagement;

[ObjectType<GetPermissionListResultDto>]
public static partial class GetPermissionListResultDtoNode
{
    [Query]
    [GraphQLName("permissions")]
    public static Task<GetPermissionListResultDto> GetAsync(
        string providerName,
        string providerKey,
        [Service] IPermissionAppService service)
    {
        return service.GetAsync(providerName, providerKey);
    }

    static partial void Configure(IObjectTypeDescriptor<GetPermissionListResultDto> descriptor)
    {
        descriptor.Name("PermissionList");

        descriptor
            .Field(x => x.Groups)
            .UseFiltering();
    }
}
