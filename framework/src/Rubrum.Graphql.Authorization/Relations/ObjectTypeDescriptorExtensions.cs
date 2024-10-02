using System.Reflection;
using HotChocolate.Types;
using Rubrum.Authorization.Relations;

namespace Rubrum.Graphql.Relations;

public static class ObjectTypeDescriptorExtensions
{
    private static readonly MethodInfo AddRelation = typeof(RelationDirectiveExtensions)
        .GetMethods()
        .Single(m => m is { Name: "AddRelation", IsGenericMethod: true });

    private static readonly MethodInfo AddPermission = typeof(PermissionDirectiveExtensions)
        .GetMethods()
        .Single(m => m is { Name: "AddPermission", IsGenericMethod: true });

    public static IObjectTypeDescriptor<T> BindDefinition<T>(
        this IObjectTypeDescriptor<T> descriptor,
        Type definition)
        where T : class
    {
        if (!definition.GetCustomAttributes().OfType<DefinitionAttribute>().Any())
        {
            throw new BingDefinitionNotFoundAttributeException();
        }

        foreach (var property in definition.GetProperties())
        {
            var returnType = property.PropertyType;
            var value = property.GetValue(null, null);

            if (returnType.BaseType?.IsAssignableTo<Relation>() == true)
            {
                AddRelation.MakeGenericMethod(typeof(T)).Invoke(null, [descriptor, value]);
            }

            if (returnType.IsAssignableTo<PermissionLink>())
            {
                AddPermission.MakeGenericMethod(typeof(T)).Invoke(null, [descriptor, value]);
            }
        }

        return descriptor;
    }
}
