using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rubrum.Graphql;

public static class GraphqlExtensions
{
    public static IRequestExecutorBuilder GetGraphql(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IRequestExecutorBuilder>();
    }

    public static IRequestExecutorBuilder GetGraphql(this IServiceProvider services)
    {
        return services.GetRequiredService<IRequestExecutorBuilder>();
    }
}
