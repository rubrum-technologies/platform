using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Rubrum.Graphql.Middlewares;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
public class UseUnitOfWorkAttribute : ObjectFieldDescriptorAttribute
{
    public UseUnitOfWorkAttribute([CallerLineNumber] int order = 0)
    {
        Order = order;
    }

    protected override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.UseUnitOfWork();
    }
}
