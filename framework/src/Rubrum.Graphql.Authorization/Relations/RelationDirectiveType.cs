using HotChocolate.Types;

namespace Rubrum.Graphql.Relations;

public class RelationDirectiveType : DirectiveType<RelationDirective>
{
    protected override void Configure(IDirectiveTypeDescriptor<RelationDirective> descriptor)
    {
        descriptor.Name("relation");
        descriptor.Location(DirectiveLocation.Object);
        descriptor.Repeatable();
    }
}
