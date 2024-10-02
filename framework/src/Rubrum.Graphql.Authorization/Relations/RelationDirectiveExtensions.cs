using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Authorization.Relations;

namespace Rubrum.Graphql.Relations;

public static class RelationDirectiveExtensions
{
    public static IObjectTypeDescriptor<T> AddRelation<T>(
        this IObjectTypeDescriptor<T> descriptor,
        Relation relation)
    {
        descriptor.Extend().OnBeforeCreate((context, _) =>
        {
            var builder = context.Services.GetRequiredService<IRelationDirectiveBuilder>();

            descriptor.Directive(builder.Build(relation));
        });

        return descriptor;
    }
}
