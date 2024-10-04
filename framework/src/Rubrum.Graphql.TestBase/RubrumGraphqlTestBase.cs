using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace Rubrum.Graphql;

public class RubrumGraphqlTestBase<TModule> : AbpIntegratedTest<TModule>
    where TModule : IAbpModule
{
    protected virtual async Task<IExecutionResult> ExecuteRequestAsync(
        Action<OperationRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
    {
        var executor = ServiceProvider.GetRequiredService<RequestExecutorProxy>();
        var scope = ServiceProvider.CreateAsyncScope();

        var requestBuilder = new OperationRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Build();

        var result = await executor.ExecuteAsync(request, cancellationToken);
        result.RegisterForCleanup(scope.DisposeAsync);
        return result;
    }
}
