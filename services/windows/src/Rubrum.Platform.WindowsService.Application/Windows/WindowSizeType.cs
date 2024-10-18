using HotChocolate.Types;

namespace Rubrum.Platform.WindowsService.Windows;

[ObjectType<WindowSize>]
public static partial class WindowSizeType
{
    static partial void Configure(IObjectTypeDescriptor<WindowSize> descriptor)
    {
        descriptor.Field(x => x.Width);
        descriptor.Field(x => x.Height);
    }
}
