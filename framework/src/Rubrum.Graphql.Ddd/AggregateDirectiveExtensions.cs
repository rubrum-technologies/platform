using HotChocolate.Types;

namespace Rubrum.Graphql.Ddd;

public static class AggregateDirectiveExtensions
{
    public static IObjectTypeDescriptor<T> Aggregate<T>(this IObjectTypeDescriptor<T> descriptor)
    {
        return descriptor.Directive(new AggregateDirective());
    }
}
