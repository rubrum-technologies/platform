using HotChocolate.Types;

namespace Rubrum.Platform.StoreAppsService.Apps;

[ObjectType<Version>]
public static partial class VersionType
{
    static partial void Configure(IObjectTypeDescriptor<Version> descriptor)
    {
        descriptor.Name("Version");
    }
}
