using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Rubrum.Authorization.Relations;

namespace Rubrum.Graphql.Relations;

public static class PermissionDirectiveExtensions
{
    public static IObjectTypeDescriptor<T> AddPermission<T>(
        this IObjectTypeDescriptor<T> descriptor,
        PermissionLink link)
    {
        descriptor.Extend().OnBeforeCreate((context, _) =>
        {
            var builder = context.Services.GetRequiredService<IPermissionDirectiveBuilder>();

            descriptor.Directive(builder.Build(link));
        });

        return descriptor;
    }
}
