using HotChocolate.Types;

namespace Rubrum.Platform.StoreAppsService.Apps;

[ObjectType<AppVersion>]
public static partial class AppVersionType
{
    static partial void Configure(IObjectTypeDescriptor<AppVersion> descriptor)
    {
        descriptor.Field(x => x.Major);
        descriptor.Field(x => x.Minor);
        descriptor.Field(x => x.Patch);
    }
}
