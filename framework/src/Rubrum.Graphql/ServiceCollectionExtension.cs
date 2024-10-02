using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rubrum.Graphql;

public static class ServiceCollectionExtension
{
    public static IRequestExecutorBuilder GetGraphql(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IRequestExecutorBuilder>();
    }
}
