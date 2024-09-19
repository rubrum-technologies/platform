using HotChocolate.Types;

namespace Rubrum.Graphql.Relations;

public class PermissionDirectiveType : DirectiveType<PermissionDirective>
{
    protected override void Configure(IDirectiveTypeDescriptor<PermissionDirective> descriptor)
    {
        descriptor.Name("permission");
        descriptor.Location(DirectiveLocation.Object);
        descriptor.Repeatable();
    }
}
