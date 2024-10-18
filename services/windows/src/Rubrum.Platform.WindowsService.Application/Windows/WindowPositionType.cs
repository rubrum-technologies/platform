using HotChocolate.Types;

namespace Rubrum.Platform.WindowsService.Windows;

[ObjectType<WindowPosition>]
public static partial class WindowPositionType
{
    static partial void Configure(IObjectTypeDescriptor<WindowPosition> descriptor)
    {
        descriptor.Field(x => x.X);
        descriptor.Field(x => x.Y);
    }
}
