using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Rubrum.Graphql.Errors;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class UseAbpErrorAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.Error<AbpErrorFactory>();
    }
}
