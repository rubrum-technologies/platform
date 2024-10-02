using HotChocolate.Resolvers;

namespace Rubrum.Graphql.Middlewares;

internal class AnyMiddleware<T>(FieldDelegate next)
    where T : class
{
    public async Task InvokeAsync(IMiddlewareContext context)
    {
        await next(context);

        context.Result = context.Result switch
        {
            IAsyncEnumerable<T> ae => await ae.AnyAsync(),
            IEnumerable<T> en => await Task.Run(() => en.Any(), context.RequestAborted).ConfigureAwait(false),
            _ => context.Result,
        };
    }
}
