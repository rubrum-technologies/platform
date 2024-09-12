using HotChocolate.Types;

namespace Rubrum.Platform.StoreAppsService.Apps;

[ObjectType<App>]
public static partial class AppType
{
    static partial void Configure(IObjectTypeDescriptor<App> descriptor)
    {
        // TODO: Описание класса
    }
}